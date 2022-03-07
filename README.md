Gigya.Files.Cli.App

Design and implement CLI that allows operations with files and directories. 
	list	
o	This command lists the contents of the current directory in a short form.
o	Bonus: 
	detailed list option
	displays all hidden files option
	makedir
o	This command creates a new directory
	remove
o	This command removes the files from the file system. Directories are not removed unless the -r option is used.
o	Bonus:
	-r option that removes any existing subdirectories
	move
o	This command copies from a source file to a target file, then removes the source file
	copy
o	This command copies a source file to a target file. 
o	Bonus:
	flag prompts for confirmation before an existing file is overwritten
	flag copies subdirectories recursively
The interface should support access by full and relative paths. 
Bonus:
	find
o	This command searches for the file in subdirectories of current directory 
