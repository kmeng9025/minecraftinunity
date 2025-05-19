using System.IO;
using NUnit.Framework;

namespace Server{
    public class FileReader{
        private static FileReader instance;
        private string worldDir;
        private string playerDir;
        private long seed;
        public FileReader(){
        }

        public void SetUp(){
            
        }

        public static FileReader getInstance(){
            if(instance == null){
                instance = new FileReader();
            }
            return instance;
        }
    }
}