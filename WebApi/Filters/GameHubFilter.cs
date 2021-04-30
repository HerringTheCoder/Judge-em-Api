using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using WebApi.Attributes;
using WebApi.Constants;
using WebApi.Hubs;

namespace WebApi.Filters
{
    public class GameHubFilter : IHubFilter
    {
        private readonly IGameService _gameService;

        public GameHubFilter(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext context,
            Func<HubInvocationContext, ValueTask<object>> next)
        {
            try
            {
                var calledMethodParameters = context.HubMethod.GetParameters().ToList();
                int gameCodeParameterIndex = calledMethodParameters.FindIndex(p => p.GetCustomAttribute(typeof(GameCodeAttribute)) != null);

                if (gameCodeParameterIndex != -1)
                {
                    if (context.HubMethodArguments[gameCodeParameterIndex] is string gameCode)
                    {
                        int gameId = await _gameService.FindActiveGameIdByCode(gameCode);
                        if (gameId == 0)
                        {
                            await context.Hub.Clients.Caller.SendAsync("SendMessage", "Could not find any active game using provided code.", MessageType.Error);
                        }
                        else
                        {
                            context.Context.Items.Add(KeyConstants.GameCode, gameId);
                        }
                    };
                }

                return await next(context);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
