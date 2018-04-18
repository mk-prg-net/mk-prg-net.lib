Martin Korneffel
Stuttgart 18.4.2018

PLX := Property List eXpressions

property list expressions are special formal languages, based on polish notation.
The syntax of PLX is very pure. PLX is used as a common serialization notation 
for structured data like JSON. Compared with JSON the syntax is more restirctes 
and the requirements for serializationare lower. 

Literals of PLX are string- and propertyname- literals.

A string is simply any word, that is not a property name nor a keyword.

A text is a list of words. Text would be embraced with the keywords #$ and #.
  #$ This is a Text. The spaces   between word are without meaning #.

Because spaces between words owithout meaning, the following text is 
semantically equivalent to the previos one:
  #$ This is a Text.     The spaces between word are without meaning #.

A property name is a string, that fullfills all requirements for regular names. 
That means, it starts with a letter, contains letters, digits and underscore but no 
whitespaces.

Keywords are tags for properties, lists and basic funcotions. A synonym for 
keywords in PLX are "functionnames".

A property assignes a name to a portion of information. I.E 
  #p Pi 3.14
  #p age 50
are properties. The keyword #p is a tag for properties.

Lists in plx enumerates properties. The keyword for list start is #_ and 
for list end is #.
  #_ #p p1 <value1> #p p1 <value2> ... #.

Values of properties can be a text or a list.
  #p MyWebsite #$ mk-prg-net.de #.
  #p MyAdress 
	#_ #p location #$ ducktown #.
	   #p street #$ feather ally 13 #.
	#.
  