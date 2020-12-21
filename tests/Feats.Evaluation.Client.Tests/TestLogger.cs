using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Feats.Evaluation.Client.Tests
{
    // too lazy, stolen from https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0#create-a-custom-logger
    [ExcludeFromCodeCoverage]
    public class TestLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;
        
        public int EventId { get; set; } = 0;
        
        public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
    }
    
    [ExcludeFromCodeCoverage]
    public class TestLogger<T> : ILogger<T>
    {
        private readonly string _name;
        private readonly TestLoggerConfiguration _config;

        public TestLogger()
        {
            this._name = typeof(T).AssemblyQualifiedName;
            this._config = new TestLoggerConfiguration();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            Console.WriteLine($"Test loggger started, let's get failing!");
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == this._config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
                            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            if (this._config.EventId == 0 || this._config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = this._config.Color;
                Console.WriteLine($"{logLevel} - {eventId.Id} " +
                                $"- {this._name} - {formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }
    }
}
