# Binary-tree-displayer
Binary tree displayer in unity c#
Hi, i made a function in unity that takes a Binode - and making it into a2d tree, but here the problem:
my so called idea to display the tree is this:

make a recursive function, and every time the level will
go up by 1, and it will also send the function a 
bool(is the right or is the left of the old node)
and then if its true it will get the root of the tree
and - the y by level * 55, then it will either x = level * 55, if it right, and if its left the x = level * -55.
