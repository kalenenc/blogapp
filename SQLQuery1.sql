--SELECT * FROM Blog LEFT JOIN comments on blog.id=comments.blogid WHERE comments.blogid = 30;
--SELECT blog.posttitle, blog.body, comments.body AS COMMENTBODY FROM Blog LEFT JOIN comments on blog.id=comments.blogid where blog.id = 36;
SELECT * FROM COMMENTS;
