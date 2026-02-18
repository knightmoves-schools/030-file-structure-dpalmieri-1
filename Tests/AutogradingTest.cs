namespace Tests;

using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

public class AutogradingTest
{
    private  string rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../../";

    [Fact]
    public void Should_Replace_The_If_Statements_With_A_Single_Call_To_The_Say_Method_On_Each_Of_The_Animals(){
      var myFiles = Directory
                     .EnumerateFiles(rootDirectory, "*.cs", SearchOption.AllDirectories)
                     .Where(s =>  !s.Contains(@"/../Tests/"))
                     .Where(s =>  !s.Contains(@"/../obj/"))
                     .Where(s =>  !s.Contains(@"/../bin/"));
  
      var rootFiles = Directory.GetFiles(rootDirectory);
      var rootDirectories = Directory.GetDirectories(rootDirectory);

      Assert.Equal(5, myFiles.Count());
    }

    [Fact]
    public void Should_Have_Class_Names_That_Match_The_Containing_File_Name()
    {
        ClassNameMatchesFileName("Trainer");
        ClassNameMatchesFileName("Animal");
        ClassNameMatchesFileName("Cat");
        ClassNameMatchesFileName("Dog");
        ClassNameMatchesFileName("Bird");
    }

    [Fact]
    public void Should_Organize_These_Files_Into_The_Following_Folder_Structure_KnightMoves_KnightLight_Pet(){
      var myFiles = Directory
                     .EnumerateFiles(rootDirectory + "KnightMoves/KnightLight/Pet/", "*.cs", SearchOption.TopDirectoryOnly)
                     .Where(s =>  !s.Contains(@"/../Tests/"))
                     .Where(s =>  !s.Contains(@"/../obj/"))
                     .Where(s =>  !s.Contains(@"/../bin/"));
  
      var rootFiles = Directory.GetFiles(rootDirectory);
      var rootDirectories = Directory.GetDirectories(rootDirectory);

      Assert.Equal(5, myFiles.Count());
    }
    
    [Fact]
    public void Should_Have_Namespaces_In_Each_File_That_Matches_The_Folder_Structure()
    {
        NamespaceMatchesFolderStructure("Trainer");
        NamespaceMatchesFolderStructure("Animal");
        NamespaceMatchesFolderStructure("Cat");
        NamespaceMatchesFolderStructure("Dog");
        NamespaceMatchesFolderStructure("Bird");
    }

    private void ClassNameMatchesFileName(string name)
    {
        var myFiles = Directory
                     .EnumerateFiles(rootDirectory, name + ".cs", SearchOption.AllDirectories)
                     .Where(s =>  !s.Contains(@"/../Tests/"))
                     .Where(s =>  !s.Contains(@"/../obj/"))
                     .Where(s =>  !s.Contains(@"/../bin/"));
        
        Assert.Equal(1, myFiles.Count());
        string content = File.ReadAllText(myFiles.First());
        Regex rx = new Regex("class.*" + name);

        bool matches = rx.IsMatch(content);

        Assert.True(matches, "should have class names that match the containing file name");
    }

    private void NamespaceMatchesFolderStructure(string name)
    {
        var myFiles = Directory
                     .EnumerateFiles(rootDirectory + "KnightMoves/KnightLight/Pet/", name + ".cs", SearchOption.TopDirectoryOnly)
                     .Where(s =>  !s.Contains(@"/../Tests/"))
                     .Where(s =>  !s.Contains(@"/../obj/"))
                     .Where(s =>  !s.Contains(@"/../bin/"));
        
        Assert.Equal(1, myFiles.Count());

        string content = File.ReadAllText(myFiles.First());
        Regex rx = new Regex("namespace.*KnightMoves\\.KnightLight\\.Pet.*;");

        bool matches = rx.IsMatch(content);

        Assert.True(matches, "should have namespaces in each file that matches the folder structure");
    }
}