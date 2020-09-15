using System;
using System.Threading.Tasks;

namespace dol_con.Views
{
    public interface IMainView
    {
        void Show(string i);
    }

    public class MainView : IMainView
    {
        public void Show(string i)
        {
            Console.WriteLine("");
            Console.WriteLine("      █████ █                                                █████ ██");                                                    
            Console.WriteLine("   ██████  █                    █                         ██████  ███                          █");                         
            Console.WriteLine("  ██   █  █                    ██                        ██   █  █ ██                         ███");                        
            Console.WriteLine(" █    █  █                     ██                       █    █  █  ██                          █");                         
            Console.WriteLine("     █  █                    ████████   ████                █  █   █                                              ███");    
            Console.WriteLine("    ██ ██             ███   ████████   █ ████ █            ██ ██  █       ███       ████     ███    ███  ████     ███");    
            Console.WriteLine("    ██ ██            █ ███     ██     ██  ████             ██ ██ █       █ ███     █  ███  █  ███    ████ ████ █   ██");    
            Console.WriteLine("    ██ ██           █   ███    ██    ████                  ██ ███       █   ███   █    ████    ██     ██   ████    ██");    
            Console.WriteLine("    ██ ██          ██    ███   ██      ███                 ██ ██ ███   ██    ███ ██     ██     ██     ██    ██     ██");    
            Console.WriteLine("    ██ ██          ████████    ██        ███               ██ ██   ███ ████████  ██     ██     ██     ██    ██     ██");    
            Console.WriteLine("    █  ██          ███████     ██          ███             █  ██     █████████   ██     ██     ██     ██    ██     ██");    
            Console.WriteLine("       █           ██          ██     ████  ██                █      ████        ██     ██     ██     ██    ██      █");    
            Console.WriteLine("   ████           █████    █   ██    █ ████ █             ████     ███ ████    █ ██     ██     ██     ██    ██");           
            Console.WriteLine("  █  █████████████  ███████     ██      ████             █  ████████    ███████   ████████     ███ █  ███   ███    ██");    
            Console.WriteLine(" █     █████████     █████                              █     ████       █████      ███ ███     ███    ███   ███   ██");    
            Console.WriteLine(" █                                                      █                                ███");                             
            Console.WriteLine("  █                                                      █                         ████   ███");                            
            Console.WriteLine("   ██                                                     ██                     ███████  ██");                             
            Console.WriteLine("                                                                                █     ████");

            Task.Delay(new TimeSpan(0, 0, 5));
        }
    }
}