# How to revert the commit (for pushed changes with additional commit)

1. First check for changes

> git status

2. Reset current changes

> git reset --hard

The git reset command is a complex and versatile tool for undoing changes.

3. We check the log to get the hash of the commit (let it be d10231a)

> git log --one-line

4. We revert the change 

> git revert <commit_hash> --no-edit 

Example:

> git revert d10231a --no-edit 

NOTE: The --no-edit option prevents git from asking you to enter in a commit message.

5. push changes

> git push

## Revert commits with hard deleting the last commit (THAT HAS NOT BEEN PUSHED)

1. First check for changes

> git status

2. Reset current changes

> git reset --hard

3. Hard revert commit

> git reset --hard HEAD~<number_of_commits_we_need_to_revert>

Example

Revert one last commit:
> git reset --hard HEAD~1

Revert two last commits:
> git reset --hard HEAD~2

## How to revert pushed changes without having additional revert commit

1. First check for changes

> git status

2. Reset current changes

> git reset --hard

3. Revert one last commit:

> git reset --hard HEAD~1

4. Push proper commit

> git push origin +<full_name_of_the_branch>

The '+' sign is present to force the push

Example:

> git push origin +master

> git push origin +task/fix-the-bug

