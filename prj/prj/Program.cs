using System.CommandLine;
using System;
using System.Collections;
using System.IO;
using System.Reflection.Metadata;



var rootCommand = new RootCommand("Root command for File Bundler CLI");
var bundleCommand = new Command("bundle", "Bundle code file to a single file");


var rspCommand = new Command("rsp", "A command that facilitates code clarity ");
rspCommand.SetHandler(() =>
{
    Console.WriteLine("hi! enter your name ,please!");
    var name = Console.ReadLine();

    Console.WriteLine("enter path or file name");
    var output = Console.ReadLine();
    var newoutput = new FileInfo(output);

    Console.WriteLine("enter the desired languages");
    var languages = Console.ReadLine();

    Console.WriteLine("are you want to see the path of the file?(y/n)");
    var n = Console.ReadLine();
    var note = (n == "y" ? true : false);

    Console.WriteLine("Do you want to sort the files by type or in alphabetical order? (type/ab)");
    var s = Console.ReadLine();
    var sort = (s == "type" ? "type" : "ab");

    Console.WriteLine("Do you want to remove the empty lines of the file? (y/n)");
    var e = Console.ReadLine();
    var empty = (e == "y" ? true : false);

    Console.WriteLine("Do you want to see your name?(y/n)");
    var au = Console.ReadLine();
    var author = (au == "y" ? name : "");


    Console.WriteLine("please enter: prj bundle @\"rsp.rsp\"");
    using (var writerRsp = File.CreateText("rsp.rsp"))
    {

        writerRsp.WriteLine($"--o {newoutput}");
        writerRsp.WriteLine($"--language {languages}");
        writerRsp.WriteLine($"--note {note}");
        writerRsp.WriteLine($"--sort {sort}");
        writerRsp.WriteLine($"--empty {empty}");
        writerRsp.WriteLine($"--author {author}");

    }


});

 //bundle options
var bundleOption = new Option<FileInfo>(new[] { "--output", "--o" }, "File path and name");

var language = new Option<string>(new[] { "--language", "--l" }, "arr for type of file")
{
    IsRequired = true
};

//הערה
var note = new Option<bool>(new[] { "--note", "--n" }, "do write routing");
var sort = new Option<string>(new[] { "--sort", "--s" }, "do sort");
var empty = new Option<bool>(new[] { "--empty", "--e" }, "to clear empty lines");
var author = new Option<string>(new[] { "--author", "--a" }, "to see the file name");

//add options
bundleCommand.AddOption(bundleOption);
bundleCommand.AddOption(language);
bundleCommand.AddOption(note);
bundleCommand.AddOption(sort);
bundleCommand.AddOption(empty);
bundleCommand.AddOption(author);


bundleCommand.SetHandler((output, language, note, sort, empty, author) =>
{
    try
    {
        //files in the folder
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

        //language
        string l = language.Replace("bin", string.Empty);
        l = language.Replace("db", string.Empty);
        l = language.Replace("debug", string.Empty);
        l = language.Replace("rsp", string.Empty);

        string[] words = l.Split(',');

        string[] realFiles = new string[files.Length];

        int k = 0;
        if (!language.Equals("all"))
        {

            for (int i = 0; i < words.Length; i++)
                words[i] = "." + words[i];

            for (int i = 0; i < files.Length; i++)
            {
                for (int j = 0; j < words.Length; j++)
                {

                    if (Path.GetExtension(files[i]).Equals(words[j]))
                    {
                        realFiles[k++] = files[i];

                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < files.Length; i++)
                if (Path.GetExtension(files[i]) != ".rsp" && Path.GetExtension(files[i]) != ".bin" && Path.GetExtension(files[i]) != ".debug" && Path.GetExtension(files[i]) != ".db")
                    realFiles[k++] = files[i];
        }

        //sort
        if (sort.Equals("ab"))
        {
            Array.Sort(realFiles);
        }
        else
            Array.Sort(realFiles, (x, y) => Path.GetExtension(x).CompareTo(Path.GetExtension(y)));


        int c = 0;
        foreach (string file in realFiles)
        {
            if (file == null)
                c++;
        }

        //read
        //מערך שכל תא בו מכיל את כל תוכן הטקסט של אותו קובץ
        var arrtxt = new string[realFiles.Length];
        //העתקה למערך את הטקסט
        for (int i = c; i < realFiles.Length; i++)
        {
            // קורא את כל שורות הטקסט מהקובץ שצוין על-ידי נתיב הקובץ ומאחסן אותן במערך מחרוזות.
            var alltext = File.ReadAllLines(realFiles[i]);

            foreach (var line in alltext)
            {
                //empty
                if (empty)
                {
                    if (!String.IsNullOrEmpty(line))
                        arrtxt[i] += line + "\n";
                }
                else
                    arrtxt[i] += line + "\n";
            }
        }


        //write
        using (StreamWriter writer = new StreamWriter(output.FullName, append: true))
        {
            if (author != null)
                writer.WriteLine($"the developer: \n//{author}\n");
            for (int i = c; i < arrtxt.Length; i++)
            {

                if (note)
                    writer.WriteLine("//" + realFiles[i], "\n");
                writer.WriteLine(arrtxt[i], "\n");

            }
        }

    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("Error:file path is invalid");
    }
    catch (Exception e)
    {
        Console.WriteLine("EROR " + e.Message);
    }
}, bundleOption, language, note, sort, empty, author);

rootCommand.AddCommand(bundleCommand);
rootCommand.AddCommand(rspCommand);
rootCommand.InvokeAsync(args);





