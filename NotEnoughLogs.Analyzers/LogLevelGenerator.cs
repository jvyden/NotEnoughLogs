using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using NotEnoughLogs.Analyzers.SyntaxReceivers;

namespace NotEnoughLogs.Analyzers;

[Generator]
public class LogLevelGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // no initialization code
    }

    private static void GenerateLogFunctions(GeneratorExecutionContext context, IEnumerable<string> names)
    {
        string code = string.Empty;
        
        foreach (string name in names)
        {
            string method = $@"
    public void Log{name}(Enum category, ReadOnlySpan<char> content)
    {{
        this.Log(LogLevel.{name}, category, content);
    }}

    public void Log{name}(Enum category, ReadOnlySpan<char> format, params object[] args)
    {{
        this.Log(LogLevel.{name}, category, format, args);
    }}

    public void Log{name}(ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {{
        this.Log(LogLevel.{name}, category, content);
    }}

    public void Log{name}(ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {{
        this.Log(LogLevel.{name}, category, format, args);
    }}
";

            code += method;
        }
        
        string sourceCode = $@"
using System;

namespace NotEnoughLogs;

#nullable enable

public partial class Logger
{{
{code}
}}";
        
        context.AddSource("Logger.Levels.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
    }

    public void Execute(GeneratorExecutionContext context)
    {
        EnumNameReceiver syntaxReceiver = new();
        
        foreach (SyntaxTree tree in context.Compilation.SyntaxTrees)
            syntaxReceiver.OnVisitSyntaxNode(tree.GetRoot());
        
        
        foreach ((string className, List<string> names) in syntaxReceiver.Enums)
            switch (className)
            {
                case "LogLevel":
                    GenerateLogFunctions(context, names);
                    break;
            }
    }
}