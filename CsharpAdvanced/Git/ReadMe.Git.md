# How to revert the commit

1. First check for changes

> git status

2. Reset current changes

> git reset --hard

We can check the log

> git log

3. Push proper commit

> git push origin +<full_name_of_the_previous_commit>

Example:

> git push origin +task/previous_commit
