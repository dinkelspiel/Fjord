using System.IO;

namespace Fjord.Modules.Misc {
    public static class data {
        public static string load_data_file(string path) {
            string file_path = game_manager.executable_path + "\\resources\\" + game_manager.asset_pack + "\\data\\" + path;
            return File.ReadAllText(file_path);
        }
    }
}