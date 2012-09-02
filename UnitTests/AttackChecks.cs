using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorNET;

namespace UnitTests
{
    [TestClass]
    public class AttackChecks
    {
        [TestMethod]
        public void XSSAttackCheck()
        {
            //ha.ckers.org XSS cheat sheet
            Dictionary<string, int> list = new Dictionary<string, int>();

            //URL encoding calculator
            list.Add("';alert(String.fromCharCode(88,83,83))//\';alert(String.fromCharCode(88,83,83))//\";alert(String.fromCharCode(88,83,83))//\";alert(String.fromCharCode(88,83,83))//--></SCRIPT>\">'><SCRIPT>alert(String.fromCharCode(88,83,83))</SCRIPT>", 610);

            //XSS locator 2
            list.Add("'';!--\"<XSS>=&{()}", 785);

            //No filter evasion
            list.Add("<SCRIPT SRC=http://ha.ckers.org/xss.js></SCRIPT>", 635);

            //Image XSS
            list.Add("<IMG SRC=\"javascript:alert('XSS');\">", 610);

            //No quotes and no semicolon
            list.Add("<IMG SRC=javascript:alert('XSS')>", 540);

            //Case insensitive
            list.Add("<IMG SRC=JaVaScRiPt:alert('XSS')>", 540);

            //HTML entities
            list.Add("<IMG SRC=javascript:alert(&quot;XSS&quot;)>", 540);

            //Grave accent
            list.Add("<IMG SRC=`javascript:alert(\"RSnake says, 'XSS'\")`>", 575);

            //Begeek
            list.Add("<IMG \"\"\"><SCRIPT>alert(\"XSS\")</SCRIPT>\">", 505);

            //here
            list.Add("<IMG SRC=javascript:alert(String.fromCharCode(88,83,83))>", 505);

            //XSS calculator
            list.Add("<IMG SRC=&#106;&#97;&#118;&#97;&#115;&#99;&#114;&#105;&#112;&#116;&#58;&#97;&#108;&#101;&#114;&#116;&#40;&#39;&#88;&#83;&#83;&#39;&#41;>", 540);

            //Long UTF-8 Unicode
            list.Add("<IMG SRC=&#0000106&#0000097&#0000118&#0000097&#0000115&#0000099&#0000114&#0000105&#0000112&#0000116&#0000058&#0000097&#0000108&#0000101&#0000114&#0000116&#0000040&#0000039&#0000088&#0000083&#0000083&#0000039&#0000041>", 940);

            //XSS calculator
            list.Add("<IMG SRC=&#x6A&#x61&#x76&#x61&#x73&#x63&#x72&#x69&#x70&#x74&#x3A&#x61&#x6C&#x65&#x72&#x74&#x28&#x27&#x58&#x53&#x53&#x27&#x29>", 940);

            //Embedded tab
            list.Add("<IMG SRC=\"jav&#x09;ascript:alert('XSS');\">", 610);

            //ascii chart
            list.Add("<IMG SRC=\"jav&#x0A;ascript:alert('XSS');\">", 610);

            //Embedded carriage return
            list.Add("<IMG SRC=\"jav&#x0D;ascript:alert('XSS');\">", 610);

            //Multiline
            list.Add("<IMG&#x0D;SRC&#x0D;=&#x0D;\"&#x0D;j&#x0D;a&#x0D;v&#x0D;a&#x0D;s&#x0D;c&#x0D;r&#x0D;i&#x0D;p&#x0D;t&#x0D;:&#x0D;a&#x0D;l&#x0D;e&#x0D;r&#x0D;t&#x0D;(&#x0D;'&#x0D;X&#x0D;S&#x0D;S&#x0D;'&#x0D;)&#x0D;\"&#x0D;>&#x0D;", 575);

            //vim
            list.Add("perl -e 'print \"<IMG SRC=java\0script:alert(\"XSS\")>\";' > out", 645);

            //Null
            list.Add("perl -e 'print \"<SCR\0IPT>alert(\"XSS\")</SCR\0IPT>\";' > out", 610);

            //Spaces and meta chars
            list.Add("<IMG SRC=\" &#14;  javascript:alert('XSS');\">", 610);

            //Non-alpha-non-digit
            list.Add("<SCRIPT/XSS SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", 670);

            //Non-alpha-non-digit part 2
            list.Add("<BODY onload!#$%&()*~+-_.,:;?@[/|\\]^`=alert(\"XSS\")>", 715);

            //Yair Amit
            list.Add("<SCRIPT/SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", 670);

            //Extraneous open brackets
            list.Add("<<SCRIPT>alert(\"XSS\");//<</SCRIPT>", 540);

            //No closing script tags
            list.Add("<SCRIPT SRC=http://ha.ckers.org/xss.js?<B>", 635);

            //Protocol resolution in script tags
            list.Add("<SCRIPT SRC=//ha.ckers.org/.j>", 435);

            //the following NIDS regex
            list.Add("<IMG SRC=\"javascript:alert('XSS')\"", 375);

            //Steven Christey
            list.Add("<iframe src=http://ha.ckers.org/scriptlet.html <", 235);

            //XSS with no single quotes or double quotes or semicolons
            list.Add("<SCRIPT>a=/XSS/", 435);

            //XSS locator
            list.Add("\";alert('XSS');//", 175);

            //End title tag
            list.Add("</TITLE><SCRIPT>alert(\"XSS\");</SCRIPT>", 540);

            //INPUT image
            list.Add("<INPUT TYPE=\"IMAGE\" SRC=\"javascript:alert('XSS');\">", 710);

            //BODY image
            list.Add("<BODY BACKGROUND=\"javascript:alert('XSS')\">", 575);

            //Dan Crowley
            list.Add("<BODY ONLOAD=alert('XSS')>", 540);

            foreach (KeyValuePair<string, int> xss in list)
            {
                Assert.AreEqual(xss.Value, Validator.CheckForXss(xss.Key));
            }

            //Own check
            const string xss2 = "<ScRiPt     lAnguage=JavaScript>alert(document.cookie); var hextest=%74%65%73%74; </sCrIpt>";
            Assert.AreEqual(540, Validator.CheckForXss(xss2));
        }

        [TestMethod]
        public void SQLInjectionCheck()
        {
            int attackVector = Validator.CheckForSqlInjection("';         DrOP users    --");
            Assert.AreEqual(270, attackVector);
        }
    }
}
