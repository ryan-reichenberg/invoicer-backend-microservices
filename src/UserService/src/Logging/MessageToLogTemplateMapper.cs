using System;
using System.Collections.Generic;
using Invoicer.Common.CQRS.Logging;
using UserService.Messages.Commands;

namespace UserService.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(RegisterUserCommand),     
                    new HandlerLogTemplate
                    {
                        After = "Added new user: {Id}."
                    }
                },
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}