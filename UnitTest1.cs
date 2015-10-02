using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FileSystem;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFS
{
    [TestClass]
    public class UnitTest1
    {
        Directory currentDir;

        [TestInitialize]
        public void TestInitialize()
        {
            currentDir = new Directory("[/]", true);
            currentDir.Chmod(7);
            currentDir.Mkdir("dir1");
            currentDir.Mkdir("dir2");
            currentDir.Mkdir("dir3");
            Directory mkdir = (Directory)currentDir.Cd("dir1");
            mkdir.Chmod(7);
            mkdir.Mkdir("dir1.1");

        }


        [TestMethod]
        public void Mkdir()
        {
            Assert.IsTrue(currentDir.Mkdir("Test"));
        }
        [TestMethod]
        public void MkdirPasLesDroits()
        {
            currentDir.Chmod(4);
            Assert.IsFalse(currentDir.Mkdir("Test"));
        }
        [TestMethod]
        public void MkdirNomExistant()
        {
            Assert.IsFalse(currentDir.Mkdir("dir1"));
        }


        [TestMethod]
        public void CreatNewFile()
        {
            Assert.IsTrue(currentDir.CreateNewFile("Test"));
        }
        [TestMethod]
        public void CreatNewFilePasLesDroits()
        {
            currentDir.Chmod(4);
            Assert.IsFalse(currentDir.CreateNewFile("Test"));
        }
        public void CreatNewFileFichierExistant()
        {
            Assert.IsFalse(currentDir.CreateNewFile("dir1"));
        }


        [TestMethod]
        public void Cd()
        {
            File dossierCd = currentDir.Cd("dir1");
            Assert.AreEqual(dossierCd.GetName(), "dir1");
        }
        [TestMethod]
        public void CdPermition()
        {
            currentDir.Chmod(0);
            Assert.AreEqual(currentDir.Cd("dir1"), null);
        }
        [TestMethod]
        public void CdExistePas()
        {
            Assert.AreEqual(currentDir.Cd("test"), null);
        }


        [TestMethod]
        public void Ls()
        {
            List<File> listeFils = currentDir.Ls();
            Assert.AreEqual(listeFils.Count(), 3);
        }
        [TestMethod]
        public void LsPasLesDroits()
        {
            currentDir.Chmod(0);
            List<File> listeFils = currentDir.Ls();
            Assert.AreEqual(listeFils, null);
        }


        [TestMethod]
            public void Search()
        {
            List<File> Search = currentDir.Search("dir1.1");
            Assert.AreEqual(Search[0].GetName(), "dir1.1");
        }
        [TestMethod]
        public void SearchPasLesDroits()
        {
            currentDir.Cd("dir1").Chmod(0); //le premier repertoir n'a pas les droits de lecture
            List<File> Search = currentDir.Search("dir1.1");
            Assert.AreEqual(Search.Count(), 0);
        }
        

        [TestMethod]
        public void Delete()
        {
            Assert.IsTrue(currentDir.Delete("dir1"));
        }
        [TestMethod]
        public void DeleteFlieExistePas()
        {
            Assert.IsFalse(currentDir.Delete("test"));
        }
        [TestMethod]
        public void DeletePasLesDroits()
        {
            currentDir.Chmod(0);
            Assert.IsFalse(currentDir.Delete("dir1"));
        }


        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual( currentDir.GetName(), "[/]");
        }


        [TestMethod]
        public void RenameTo()
        {
            File FileChangerNom = currentDir.Cd("dir1");
            Assert.IsTrue(FileChangerNom.RenameTo("//"));
        }
        [TestMethod]
        public void RenameToNomNull()
        {
            File FileChangerNom = currentDir.Cd("dir1");
            Assert.IsFalse(FileChangerNom.RenameTo(""));
        }
        [TestMethod]
        public void RenameToPasDroitsParent()
        {
            currentDir.Chmod(4);
            File FileChangerNom = currentDir.Cd("dir1");
            Assert.IsFalse(FileChangerNom.RenameTo("test"));
        }
        [TestMethod]
        public void RenameToNomExistant()
        {
            
            File FileChangerNom = currentDir.Cd("dir1");
            Assert.IsFalse(FileChangerNom.RenameTo("dir2"));
        }
        

        [TestMethod]
        public void IsDirectory()
        {

            Assert.IsTrue(currentDir.IsDirectory());
        }
        [TestMethod]
        public void IsFile()
        {

            Assert.IsFalse(currentDir.IsFile());
        }
        [TestMethod]
        public void GetParent()
        {
            Directory Fils = (Directory)currentDir.Cd("dir1");
            Directory parent = Fils.GetParent();
            Assert.AreEqual(parent.GetName(), "[/]");
        }
        [TestMethod]
        public void GetPath()
        {
            Directory Fils1 = (Directory)currentDir.Cd("dir1");
            Directory Fils2 = (Directory)Fils1.Cd("dir1.1");
            string fdhsjkq = Fils2.GetPath();
            Assert.AreEqual(Fils2.GetPath(), "\\[/]dir1\\dir1.1");
        }
        [TestMethod]
        public void  CanWrite()
        {
            Assert.IsTrue(currentDir.CanWrite());
        }
        [TestMethod]
        public void CanExecute()
        {
            Assert.IsTrue(currentDir.CanExecute());
        }
        [TestMethod]
        public void CanRead()
        {
            Assert.IsTrue(currentDir.CanRead());
        }
        [TestMethod]
        public void Chmod()
        {
            currentDir.Chmod(0);
            
            Assert.IsFalse(currentDir.CanExecute());
            Assert.IsFalse(currentDir.CanWrite());
            Assert.IsFalse(currentDir.CanRead());
        }
        [TestMethod]
        public void GetRoot()
        {
            Directory Fils1 = (Directory)currentDir.Cd("dir1");
            Directory Fils2 = (Directory)Fils1.Cd("dir1.1");
            Directory currentdir2 = (Directory)Fils2.getRoot();
            Assert.AreEqual(Fils2.getRoot(), Fils1);
        }

    }
}
