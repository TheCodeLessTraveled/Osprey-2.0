# Osprey
![GitHub all releases](https://img.shields.io/github/downloads/TheCodeLessTraveled/Osprey-2.0/total?color=green)

<p>Osprey is a Windows application tool that allows you to conveniently  drag folders into a single parent window and get a bird’s eye, tiled view of all your folders. Get a tiled layout of your windows - or cascade layout. Minimize, resize and maximize the parent window or drag the entire group to another screen. Use Osprey as an ad hoc tool or save your folder layouts to conveniently reload as needed.
 </p>
 <p><b>PROBLEM EXPLAINED</b>
 <br>Do you ever wish your File Explorer windows were grouped into one parent window that would function as one window? If you are like me, you reopen File Explore countless times because you've lost track of folders in you cluttered taskbar. <b>Time-after-time</b> you sift through the heap of taskbar icons to reposition your folders buried under a myriad of application windows.</p>
 <p><b>PROBLEM SOLVED</b>
 <br>Osprey allows you to group your folders into one parent window like a purposeful team. You always know where they are. Assign your folders to a team of folders, save the folder paths, and launch these teams as needed. And, the folders are contained within a single parent window, tiled or cascaded, and in the precise order they were saved. You just need to first build your folder teams, but, that's easy. Browse to each folder and save as you go – one time. Add a new folders, remove members, save these adjustments as you go.
</p>
<p>
Launch Osprey and select your team.  Reload each team as needed. Your File Explorer windows are neatly organized in a single parent window, <b>time-after-time</b>. 
</p>
 
 - Reload a team with [File] -> [Open] -> click. 
 - Move the entire team to a different screen.
 - Resize the entire team.
 - Minimize the team parent window.
 - Launch multiple instances of Osprey and display several teams on different screens.
<br>
The folders stay in place. No more hunting for that one, lost folder buried in the pile of icons that clutter your taskbar. 
<br>
<br>

<b>USAGE EXAMPLE:</b><br>
As an ETL developer or a production support engineer, I might monitor 6 folders for one ETL called, "The Topaz Data Files Load". 
<br>
Build your Topaz team like this:
<br>
1.  Open Osprey. 
2.  [File] -> [Add New Explorer].
            Browse to your first folder member. Repeat this for the remaining 5 folders.

3.  [Save As] -> Type "Topaz" in the textbox.
            Press [Enter] key. 
            <p>Team “Topaz” is created with its 6 members. The Topaz group is saved to 
            OspreyData.xml. The XML node is named "Topaz". It has 6 child nodes, each
            containing their respective folder path. Your saved team can be selected the next time you need this team of folders opened.</p>

<b>ACTIONS:</b><br>      

<table><tr> 
 <td Width="20%">[Save] or [Save As] </td>
 <td Width="80%">  Whenever you [Save] or [Save As], Osprey will act upon what is currently loaded
                   into the parent window. So, you can have Topaz loaded, modify paths, add
                   additional folders and when satisfied with adjustments, <br>
                   <p>click [Save As] -> "Diamond". Press [Enter] key. <br>
                   Now there are two collections of folders that can be 
                   reloaded. These are saved in the OperyData.xml file as two nodes.
                    </p>
</td></tr>
<tr> 
    <td Width="20%">[Open]</td>
    <td Width="80%"> 
        <p>When you click [Open], a ComboBox becomes visible and lists all saved folder 
        teams. You might have the two teams from above, "Topaz" and "Diamond". Select one 
        from the dropdown list to load its group of folders into the parent window.
     </p>
    </td>
  </tr>
<tr>
    <td Width="20%">[Rename]</td> 
    <td Width="80%"> 
     <p>
        Click [Rename] and a textbox becomes visible. Give Team “Topaz” a new name by 
        typing "Ruby" in the textbox. Press [Enter] and the OspreyData.xml “Topaz” node is
        renamed to “Ruby”.
     </p>
    </td>
</tr>
<tr>
    <td Width="20%">[Reset OspreyData.xml] </td>    
    <td Width="80%"> 
     <p>
        This was handy during development. I don't know how useful it is in    
        practice. But, someone just might want to "factory reset" their xml file. 
        Have at it.
     </p>
     <p>A WORD OF CAUTION:  I created this for IT Pros and purposefully omitted pop-up warnings 
        and progress bars to make this a simple tool - like a hammer. Avoid accidental deletion 
        or resetting your OpseryData.xml file. Consider saving an extra copy of your OspreyData.xml file.
     </p>
    </td>
</tr>
<tr>  
    <td Width="20%">OspreyData.xml</td>     
    <td Width="80%"> 
     <p>
        This is a very simple XML file and if you know a thing or two about XML, you should
        feel comfortable editing this file. Why, you ask? You can change the order of how your folders 
        load and display. You've probably experinced how XML parses backwards. Keep this in mind
        if you want to change the order of your folders because the folder paths will load into the parent 
        window from bottom to top. So, reorder the child nodes accordingly.
     </p>
    </td>
</tr>
</Table>
