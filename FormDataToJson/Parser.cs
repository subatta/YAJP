using System;
using System.Linq;

namespace FormDataToJson
{
    public class Parser
    {

        const string DblQ = "\"";
        const string OCBrace = "{";
        const string CCBrace = "}";
        const string OSBrace = "[";
        const string CSBrace = "]";
        const string Colon = ":";
        const string Comma = ",";
        const string UnderScore = "_";
        const char Amp = '&';
        const char Eq = '=';

        NodeCollection items = new NodeCollection();
        string json = string.Empty;

        public string JsonFromString(string inStr)
        {
            var kvp = inStr.Split(Amp);
            foreach (var i in kvp)
            {
                var pair = i.Split(Eq);
                items.AddEntry(pair[0], 0, pair[1]);
            }

            json += OCBrace;
            traverse(items);
            json += CCBrace;

            json = json.Replace(Comma + CCBrace, CCBrace);    // remove trailing comma for valid json object
            json = json.Replace(Comma + CSBrace, CSBrace);    // remove trailing comma for valid json array

            return json;
        }

        void traverse(NodeCollection root)
        {
            foreach (var n in root)
            {
                if (n.Value.HasChildren)
                {
                    if (n.Value.Children.Count(x => x.Key.Contains(UnderScore)) > 0)
                    {
                        json += (DblQ + n.Key + DblQ + Colon + OSBrace);
                        n.Value.IsList = true;
                    }
                    else
                    {
                        if (n.Key.Contains(UnderScore))
                        {
                            json += OCBrace;
                        }
                        else
                        {
                            json += (DblQ + n.Key + DblQ + Colon + OCBrace);
                        }
                    }

                    traverse(n.Value.Children);

                    if (n.Value.IsList)
                    {
                        json += (CSBrace + Comma);
                    }
                    else
                    {
                        json += (CCBrace + Comma);
                    }
                }
                else
                {
                    json += (DblQ + n.Key + DblQ + Colon + DblQ + n.Value.Data + DblQ + Comma);
                }
            }
        }
    }
}
