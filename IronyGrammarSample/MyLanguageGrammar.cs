using System;
using Irony.Parsing;

namespace IronyGrammarSample
{
    [Language("InnoSetup Script", "1.0", "Grammar for InnoSetup Script ISS file.")]
    internal class MyLanguageGrammar : Grammar
    {
        public MyLanguageGrammar()
        {
            // Terminals
            var colon = ToTerm(":");
            var semicolon = ToTerm(";");
            var source = ToTerm("Source", "SourceParamName");
            var value = new DsvLiteral("DSV", TypeCode.String, ";");

            //Non-terminals
            var parameterName = new NonTerminal("Parameter Name");
            var parameterValue = new NonTerminal("Parameter Value");
            var parameter = new NonTerminal("Parameter");
            var line = new NonTerminal("Line");
            var lines = new NonTerminal("Lines");
            var script = new NonTerminal("Script");

            //Rules
            parameterName.Rule = source;
            parameterValue.Rule = value;
            parameter.Rule = parameterName + colon + parameterValue;
            line.Rule = MakeStarRule(line, semicolon, parameter);
            lines.Rule = MakeStarRule(lines, NewLine, line);

            script.Rule = lines;

            Root = script;
            this.LanguageFlags |= LanguageFlags.NewLineBeforeEOF;

            //  var sectionTerm = new NonTerminal("Section");
            //  sectionTerm.Rule = ToTerm("[") + sectionIndentifier + "]";
            //
            //  var comment = new CommentTerminal("comment", ";", "\n", "\r");
            //  base.NonGrammarTerminals.Add(comment);
            //
            //  var stringLiteral = new FreeTextLiteral("StringFieldValue",FreeTextOptions.None, ";");
            //  var fieldValue = new NonTerminal("fieldValue");
            //  fieldValue.Rule = stringLiteral;
            //  MarkTransient(fieldValue);
            //
            //  var parameter = new NonTerminal("Parameter");
            //  var fieldName = new IdentifierTerminal("Parameter Name");
            //  parameter.Rule = fieldName + ":" + fieldValue;
            //
            //  var parameterLine = new NonTerminal("Parameter Line");
            //  parameterLine.Rule =  MakePlusRule(parameterLine, ToTerm(";"), parameter);
            //
            //  var parameterLines = new NonTerminal("Parameter Lines");
            //  parameterLines.Rule = MakePlusRule(parameterLines, parameterLine);
            //
            //  var sectionBlock = new NonTerminal("Section Block");
            //  sectionBlock.Rule = sectionTerm + parameterLines;
            //
            //  var sectionList = new NonTerminal("Section List");
            //  MakePlusRule(sectionList, sectionBlock);
            //
            //  var root = new NonTerminal("Script");
            //  root.Rule = sectionList | parameterLine;
            //  this.Root = root;
        }
    }
}