using System;
using Client.Presenter;

namespace Client
{
    static class CMDUser
    {
        static MainPresenter _presenter;

        public static void InitPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        public static bool IsCommand(string text)
        {
            bool isCommand = false;

            if (text[0] != '/')
                return isCommand;

            switch (text)
            {
                case "/about":
                    _presenter.BotCommand("Copyright © 2017 G4BY\n        All Rights Reserved.");
                    isCommand = true;
                    break;

                case "/clear":
                    _presenter.BotCommand("/clear");
                    isCommand = true;
                    break;

                case "/time":
                    _presenter.BotCommand(DateTime.Now.ToString());
                    isCommand = true;
                    break;

                case "/help":
                    _presenter.BotCommand("Available commands\n\t/about -- About developer and copyright" +
                       "\n\t/clear -- Clear the cheat room" + "\n\t/time -- Display date and time");
                    isCommand = true;
                    break;

            }
            return isCommand;
        }
    }
}
