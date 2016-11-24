select blog.PostTitle, blog.body, comments.body from blog
join comments
on blog.id=comments.id;

