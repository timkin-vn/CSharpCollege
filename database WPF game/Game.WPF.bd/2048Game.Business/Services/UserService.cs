using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using _2048Game.Business.Models;
using System.Linq;

namespace _2048Game.Business.Services
{
    public static class UserService
    {
        private static readonly string DataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SokobanGame");

        static UserService()
        {
            if (!Directory.Exists(DataDir)) Directory.CreateDirectory(DataDir);
        }

        public static bool UserExists(string username)
        {
            var path = GetPath(username);
            return File.Exists(path);
        }

        public static string GetFilePathForUser(string username)
        {
            var safe = string.Join("_", username.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(DataDir, safe + ".xml");
        }

        public static void SaveBoard(string username, SokobanBoard board)
        {
            if (board == null) return;
            var path = GetPath(username);
            var dto = BoardDto.FromBoard(board);
            var xs = new XmlSerializer(typeof(BoardDto));
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs, Encoding.UTF8))
            {
                xs.Serialize(writer, dto);
            }
        }

        public static SokobanBoard LoadBoard(string username)
        {
            var path = GetPath(username);
            if (!File.Exists(path)) return null;
            var xs = new XmlSerializer(typeof(BoardDto));
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs, Encoding.UTF8))
            {
                var dto = (BoardDto)xs.Deserialize(reader);
                return dto?.ToBoard();
            }
        }

        private static string GetPath(string username)
        {
            var safe = string.Join("_", username.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(DataDir, safe + ".xml");
        }

        [Serializable]
        public class BoardDto
        {
            public int Rows { get; set; }
            public int Cols { get; set; }
            public int[] CellsFlat { get; set; }
            public int PlayerR { get; set; }
            public int PlayerC { get; set; }

            public static BoardDto FromBoard(SokobanBoard b)
            {
                var dto = new BoardDto { Rows = b.Rows, Cols = b.Cols, PlayerR = b.PlayerPosition.r, PlayerC = b.PlayerPosition.c };
                dto.CellsFlat = new int[b.Rows * b.Cols];
                for (int r = 0; r < b.Rows; r++)
                    for (int c = 0; c < b.Cols; c++)
                        dto.CellsFlat[r * b.Cols + c] = b.Cells[r, c];
                return dto;
            }

            public SokobanBoard ToBoard()
            {
                var board = new SokobanBoard(Rows, Cols);
                for (int r = 0; r < Rows; r++)
                    for (int c = 0; c < Cols; c++)
                        board.Cells[r, c] = CellsFlat[r * Cols + c];
                board.PlayerPosition = (PlayerR, PlayerC);
                return board;
            }
        }
    }
}
