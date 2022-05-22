namespace CsharpAdvanced.VisualStudio;

public class Shortcuts
{
    //In this section we will focus on the useful general shortcuts (build in)
    //symbol + will be used to underline the fact that we need to click additional key
    //symbol -> will be used to show the result of the shortcut
    //brackets [] will be use to tell about the action that should be done before the shortcut
    //brackets <> will be use to tell about possible keys that are determined by the word inside like <arrow> means a arrow key

    //ctrl + d                              -> duplicate the line
    //ctrl + a                              -> select all
    //ctrl + /                              -> comment line or lines by "//"
    //ctrl + f                              -> find something
    //ctrl + shift + f                      -> advanced find and replace
    //ctrl + t                              -> find class
    //ctrl + .                              -> to open the vs helper window (use all the time). Do not need to be exactly on the piece of code
    //ctrl + m + f                          -> find current file in the solution (CodeMaid)
    //ctrl + m + z                          -> reorganize file (CodeMaid)
    //ctrl + m + space                      -> cleanup active document (CodeMaid)

    //tab                                   -> make tab
    //shift + tab                           -> undo tab

    //ctrl + alt + <mouse click>            -> make another cursor (can have multiple)
    //alt + shift + <down/up arrow>         -> make another cursor (can have multiple) up or down

    //ctrl + x                              -> cut current line (good for deleting current line)
    //ctrl + c                              -> copy selected
    //ctrl + v                              -> paste

    //ctrl + z                              -> Undo
    //ctrl + shift + z                      -> Redo
    //ctrl + y                              -> Redo

    //alt + <down/up arrow>                 -> move the current line down or up
    //ctrl + <right/left arrow>             -> move to the next string (fast walking)
    //shift + <right/left arrow>            -> move by character and select it
    //ctrl + shift + <right/left arrow>     -> move by string and select it
    //ctrl + <delete/backspace>             -> delete a string before (backspace) or next (delete)

    //ctrl + end                            -> move to the end of the document
    //ctrl + home                           -> move to the beginning of the document
    //ctrl + g                              -> move to specified line

    //ctrl + s                              -> save current tab in the file
    //ctrl + shift + s                      -> save all tabs in the file

    //alt + tab                             -> change window
    //alt + shift + tab                     -> change window in the opposite direction

    //ctrl + tab                            -> change tab in current window (only tab)
    //ctrl + shift + tab                    -> change tab in current window in the opposite direction

    //shift + enter                         -> go to new line and fill the semicolon
    //alt + enter                           -> implement interface or base class

    //[put cursor on the piece of code] F1  -> move to the online documentation of the current code
    //[put cursor on the piece of code] F2  -> rename (custom made)
    //F5                                    -> start debugging
    //ctrl + F5                             -> run application
    //F9                                    -> set or remove breakpoint
    //F10                                   -> step over
    //F11                                   -> step into
    //[put cursor on the piece of code] F12 -> move to the source definition of the current code

    //alt + f                               -> create new folder (custom made)
    //alt + n                               -> create new class (custom made)
    //ctrl + p                              -> open PowerShell (custom made)

    //ctrl + m + m                          -> collapse current scope

    //[when intellisense is open] alt + p   -> narrow results to properties
    //[when intellisense is open] alt + m   -> narrow results to methods
    //[when intellisense is open] alt + e   -> narrow results to enums
    //[when intellisense is open] alt + c   -> narrow results to classes

    //alt + F4                              -> close current window
    //ctrl + F4                             -> close current tab in the window

    //ctrl + -                              -> navigate backward
}