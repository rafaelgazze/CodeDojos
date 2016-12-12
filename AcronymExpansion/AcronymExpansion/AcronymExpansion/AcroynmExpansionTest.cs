using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace AcronymExpansion
{
    [TestFixture]
    public class AcroynmExpansionTest
    {
        [TestCase("")]
        [TestCase(null)]
        public void AcroynmExpansionTest_Verify_Phrase_Is_NotNull_Or_Empty(string input)
        {
            AcroynmExpansion parser = new AcroynmExpansion();
            Assert.Throws<ArgumentNullException>(() => parser.TranslatePhrase(input));
        }

        [TestCase("gg", "good game")]
        [TestCase("dw", "don't worry")]
        [TestCase("hf", "have fun")]
        [TestCase("lol", "laugh out loud")]
        [TestCase("brb", "be right back")]
        [TestCase("g2g", "got to go")]
        [TestCase("gl", "good luck")]
        [TestCase("wp", "well played")]
        [TestCase("imo", "in my opinion")]
        public void AcroynmExpansionTest_Translate(string abreviation, string fullphrase)
        {
            AcroynmExpansion parse = new AcroynmExpansion();
            string result = parse.TranslatePhrase(abreviation);

            Assert.That(fullphrase, Is.EqualTo(result));
        }



        [Test]
        public void AcroynmExpansionTest_Translate_PharseFull_HaveAbreviation()
        {
            AcroynmExpansion parse = new AcroynmExpansion();
            string result = parse.TranslatePhrase("gg hf teste 1");
            Assert.That("good game have fun teste 1", Is.EqualTo(result));
        }

        [Test]
        public void AcroynmExpansionTest_Translate_PharseFull_NoAbreviation()
        {
            AcroynmExpansion parse = new AcroynmExpansion();
            string result = parse.TranslatePhrase("there's a global variable over there");
            Assert.That("there's a global variable over there", Is.EqualTo(result));
        }
    }

    public class AcroynmExpansion
    {
        private readonly Dictionary<string, string> _abreviation = new AttributeDictionary
        {
            {"lol","laugh out loud" },
            {"dw", "don't worry"},
            {"hf","have fun" },
            {"gg","good game" },
            {"brb","be right back" },
            {"g2g","got to go" },
            {"wp","well played" },
            {"gl","good luck"},
            {"imo","in my opinion" }
        };



        public string TranslatePhrase(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                throw new ArgumentNullException();

            var splitWords = phrase.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            return ComputeWords(splitWords);
        }

        private string ComputeWords(string[] words)
        {
            var result = "";
            foreach (var word in words)
            {
                var completeWord = _abreviation.FirstOrDefault(t => t.Key == word).Value;
                result += string.IsNullOrEmpty(completeWord) ? word + " " : completeWord + " ";
            }

            return result.Trim();
        }


    }
}
