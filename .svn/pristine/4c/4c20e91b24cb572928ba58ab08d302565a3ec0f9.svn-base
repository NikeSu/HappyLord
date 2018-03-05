import os
import string
 
dirName = os.getcwd()
print(dirName)
 
li=os.listdir(dirName)
for filename in li:
    newname = filename
    #print (newname)
    filtername = newname.split(".")
    if filtername[-1]=="atlas":
        print (filtername)
        newname = newname + ".txt"
        os.rename(filename,newname)
        print newname,  "+txt successfully"
 
    if filtername[-1]=="json":
        print (filtername)
        newname = newname + ".txt"
        os.rename(filename,newname)
        print newname,  "+txt successfully"  