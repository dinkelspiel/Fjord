using System.IO;

namespace Fjord.Modules.Misc {
    public static class data {
        public static string load_data_file(string path) {
            string file_path = game.executable_path + "\\" + game.get_resource_folder() + "\\" + game.asset_pack + "\\data\\" + path;
            return File.ReadAllText(file_path);
        }
    }
}