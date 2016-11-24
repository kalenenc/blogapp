using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDBConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please enter your username");
            string UserName = Console.ReadLine();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=c:\users\tina\documents\visual studio 2015\Projects\BlogDBConsole\BlogDBConsole\Database1.mdf;Integrated Security=True");
            connection.Open();
            SqlCommand UserNameCommand = new SqlCommand($"INSERT INTO [Users] (UserName) VALUES ('{UserName}'); SELECT @@IDENTITY AS id;", connection);
            SqlDataReader reader = UserNameCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine($"Awesome. Your username is {UserName} and stored with an User ID of  {reader["id"]}");
                reader.Close();
            }

            SqlCommand ListUserNames = new SqlCommand("SELECT * from Users", connection);
            SqlDataReader UserNameReader = ListUserNames.ExecuteReader();
            if (UserNameReader.HasRows)
            {
                while(UserNameReader.Read())
                {
                    Console.WriteLine(UserNameReader["username"] + " " + UserNameReader["id"]);
                }
                UserNameReader.Close();
            }
            while (true)
            {
                Console.WriteLine("Do you want to a.) write a blog post or b.) view a blog post?");
                string answer = Console.ReadLine().ToLower();

                if (answer == "a")
                {
                    Console.WriteLine("Please enter the title of your blog post");
                    string title = Console.ReadLine();
                    Console.WriteLine("Please enter the body post you want to enter");
                    string body = Console.ReadLine();
                    SqlCommand NewBlogCommand = new SqlCommand($"INSERT INTO [Blog] (posttitle, Body) VALUES ('{title}', '{body}'); SELECT @@IDENTITY AS id;", connection);
                    SqlDataReader BlogReader = NewBlogCommand.ExecuteReader();
                    if (BlogReader.HasRows)
                    {
                        BlogReader.Read();
                        int BlogID = Convert.ToInt32(BlogReader["id"]);
                     
                        Console.WriteLine("Your blog post is and stored with an User ID of " + BlogReader["id"]);
                        BlogReader.Close();
                    }
                }
                else if (answer == "b")
                {
                    Console.WriteLine("Type in the id number of the blog post you want to view");
                    string BlogID = Console.ReadLine();
                    SqlCommand RetrieveBlogCommand = new SqlCommand($"SELECT blog.posttitle, blog.body, comments.body AS COMMENTBODY FROM Blog LEFT JOIN comments on blog.id=comments.blogid where blog.id = {BlogID}", connection);
                    SqlDataReader RetrieveReader = RetrieveBlogCommand.ExecuteReader();
                    if (RetrieveReader.HasRows)
                    {                      
                        while(RetrieveReader.Read())
                        {
                            Console.WriteLine(RetrieveReader["posttitle"] + ": " + RetrieveReader["Body"] + RetrieveReader["COMMENTBODY"]);
                        }
                        RetrieveReader.Close();
                    }
                    Console.WriteLine("Do you want to write a comment?");
                    string thisanswer = Console.ReadLine().ToLower();
                    if (thisanswer == "yes")
                    
                        Console.WriteLine("Please enter the comment you want to add");
                        string newComment = Console.ReadLine();
                        SqlCommand addComment = new SqlCommand($"INSERT INTO Comments (body, BLOGID) VALUES ('{newComment}', '{BlogID}') ; SELECT @@Identity AS ID;", connection);
                        SqlDataReader CommentReader = addComment.ExecuteReader();
                        if (CommentReader.HasRows)
                         {
                             CommentReader.Read();
                             Console.WriteLine($"Cool, your comment was saved with an ID of '{CommentReader["ID"]}'");
                             
                         }
                    CommentReader.Close();
                } 
                else if (answer == "exit")
                {
                    Console.WriteLine("Bye");
                    //statement = false;
                }
            }
            Console.ReadLine();
            connection.Close();

        }
    }
}
