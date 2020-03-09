# Git Bash Workflow
Hey all peeps, after being approached several times, and for my own benefit, I've created this guide to be used as a general reminder for working with the git bash shell- and merging code correctly. Hopefully this is helpful to some of you who are either struggling or could use a good reference.

## Git Bet Gash running

First step is simple, right? Opening a program is elementary. ***However***, navigating directories within a shell can be a pain. Here's a quick trick to speed up initial navigation.

 1. Navigate to your repository folder with **File Explorer**
 2. Click the **path** 
![alt text](https://i.imgur.com/qrB6j44.png)
3. Type ```git bash```
![alt text](https://i.imgur.com/e0JtJcQ.png)
4. Hit **enter**
5. ***Voilà***, git bash is open in your directory.

## Add and Commit your feature work
Before doing anything, you should add and commit any untracked work you've done. While on your feature branch:

 1. Type ``git add .`` to track all modified files within the directory
 2. Type ``git commit -m "Your commit message here"``to commit the work to that branch
## Verify dev is up-to-date
Alright, our work is tracked. Next, we need to make sure our dev branch is up-to-date. Here's how:

 1. Type ``git checkout dev``
 2. Type  ``git pull origin dev``
Note: This is assuming you're the main repository owner. If you've forked the project, to update from the master project you should have setup an ``upstream`` remote host- your command would then be ``git pull upstream dev``

Congratz, if all is well your ``dev`` branch is up-to-date.

## Merge the current dev into your feature
Here is where it starts to get a bit sticky for some. We now need to merge our updated dev into our feature branch and check/fix your conflicts (so the main repo owner doesn't have to!):

 1. Type ``git checkout featurebranch``, where featurebranch is your feature branch. STONKS AMIRITE
 2. While on your feature branch, type ``git merge dev``. This will attempt to merge your updated dev branch into your feature branch. If you're super lucky, there may be no conflicts! Unfortunately there will be some, as this is real life.
 3. Your feature branch name will add subtext to it while merging- this will look something like this: ``(featurebranch | MERGING)``. Git bash will also automatically look for a tool to use for merging. For most, it will default to **vimdiff**.  From here:
 4. Type ``git mergetool`` to start the merge process. This will pop open a window with **vimdiff**- showing your feature code, the dev code, some temporary code, and the attempted merged code. From here, I recommend just removing the dividers **vimdiff** inserts and fixing your conflicts after merging. You navigate **vimdiff** just like you would a **vi** or **vim** editor.
 5. Remove the lines that look like ``>>>>>HEAD``, ``=========``, ``<<<<<<dev``. Sometimes it will look like a commit hash (e.g.``>>>>>>5c6b7106779a124685``)- remove that too.
 6. Once the dividers are removed, hit your **escape** key, then type ``:wqa`` and hit **enter**. It will then place you back in the shell and tell you if there are any more conflicted files. If there are more, start again from step 4. If not, then commit your merged files by typing ``git commit -m "Merge message"``. The feature branch name should now look normal and no longer have ``| MERGING``.
 7. Open up **visual studio** and fix your conflicted code and test now. The reason we are doing it now and not while merging is 1. It's ***hella*** easier to navigate the editor, and 2. You can test it for errors and actually make sure ***it's working as intended***.
 8. Once you're finished fixing the conflicts, add and commit your changes to your feature branch.

## Push and Pull Request
Well, if you've made it this far, you've updated your dev branch, merged your code, fixed any issues, added and committed them, and your feature branch is ready to push. On your feature branch:

 1. Type ``git push origin featurebranch`` to push your work up to your hosted repository. You need to do this whether you own the master or forked the master. 
 2. On GitHub, navigate to your feature branch and click **New pull request**.  Set it to your feature branch asking to merge changes into the main repository owners **dev** branch. This is the same if you're the main repository owner- you should still do a pull request from your feature branch to your dev branch.
 3. Your work is now done and ready for the main repository owner to integrate. Yes, if you're the main repository owner, you just sent yourself a pull request. It's a tad silly but ***it keeps the workflow consistent and able to backtrack correctly.***

##
I hope this helps some of you out. I'll probably throw some more pictures in when I do my next cycle. 

&copy; 2020 - Michael Caldwell | Digital Hijinks
