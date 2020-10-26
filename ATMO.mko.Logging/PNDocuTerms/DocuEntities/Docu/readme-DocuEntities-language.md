19.4.2018
Martin Korneffel

# DocuEntity Lib

DocuEntites is a lib to create a strutured documentation of a program state. The documentation of program states becomes important in case of failures
or execution of critical programm parts.
The programm state will be expressed with states of involved parties like objects, methods and  properties.
The basic serialization format for DocuEntites is polisch notation (PN). In PN a expression starts with a specific statement.
To exactly distinguish between literals an expression prefixes, every prefix starts with a #. 

## Whats New
6.12.2018
Martin Korneffel

Beginning from mko.RPN Tokenizer will automaticly decode from RPNUrlSaveEncoded Strings. 


## Literals

### Date
A date literal express the day, month and year components of a point in time.

PN:

    #d 31.12.2018

### Time
A time literal express the hour, minute and second components of point in time.
PN: #t 10:59:23

### String
A string literal is simply a chain of letter, digit and interpunctuation signs exept whitspaces.

### Text
A Text is a list of strings, separated by whitespaces. To separate two strings in a text, on whitespace is enought. 
During deserialization count of whitespaces between two strings will be reduced to one anytime.
Because the length of string can varying, strings are terminated in PN with #. symbol.

PN: 

	#$ This is a text #.

### List
A list comprises a set of properties, methods and instances. Like strings the amount of list members are varying, so 
also lists are terminated by symbol #..
PN: #_ #p p1 #$p1.val #. #m m2 #_ ... #. #.


# DocuEntity can be on of the following

## Property
Assignes a name to a portion of information.
A portion of information can be a text, a list or a instance.
PN: #p PI #$ 3.14 #.

## Version
Defines a version numeber for a business object like instances.
The version number consists of thre parts: main, sub and build- number.
The parts are separated with points (i.e. 1.2.3).
PN: #v 1.2.3

## Event
An Event can indicate the success of an operation on an business object. 
The structure of an event is equivalent to the structure of a property: #e name value.
The name is often an indicator for success: succeded, failed, warn, ... se DocuEntityHlp.MapStringToEventType

PN: 

    #e succeded 
    #e fails #i Exception #_ #p msg #$ somthing goes wrong #. #.

## Instance
A instance defines a block, that decribes a business object.
It has a name and contains a list with properties, methods and events or a version number.

PN:

    #i <name> #_ <member 1> ... #.

## Method
A method documents a method- or function call an the results of them.
It contains instances, properties and events

PN:

    #m sin #_ #p x #$ 3.14/4 #. #r #_ #e succeded #. #.


## Makros

Makros are predifined Intstance templates. The name of this instances and the sturcture are preseted.
I.e with the xTab Makro you can define a cross table. The html formatter renders with html markup a xTab Makron in a 
visual cross table.

PN:

    #i xTab #_
        #p dim1	#_ <all components of dimension 1> #.
		#p dim2	#_ <all components of dimension 2> #.
		#p values	
			#_
		    	#i <comp_d1: Name of dimension 1 component> 
				    #_
						#p <comp_d2: Name of dimension 2 component> 
							#$ <value of (comp_d1, comp_d2)> ... #.						
						#.
            		#.


















