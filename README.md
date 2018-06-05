#########################################################
#_________________Piano-Hero-Unity-3D___________________#
#########################################################

(Work in progress) Code in C# for an educational game that uses MIDI within the Unity 3D game engine. Ultimately intended to replicate mechanics similar to the Guitar Hero / Rock Band series.

====================

KEY FOR ENCODING:

Symbols:

'%' — Precedes information related to constants (e.g. title, measure count, ending measure, etc.), written in the form %KEY=VALUE. [ %TITLE=MyTitle” ]

'#' — Precedes information that effects groups of measures of any length greater than 0. This can include changes in the time signature, key signature, clef, etc., written in the form #KEY:VALUE. [ #CLEF:bass ]

	#CLEF:… — Defines the clef (treble/bass)
	#KS:N.m — Defines the key signature, where N represents the letter name of the key and m represents major/minor (‘M’ = major, ‘m’ = minor)
	#TS:n.d — Defines the time signature, where n represents the numerator and d represents the denominator
	#REP:n.e — Indicates that the following measures are repeated, with n representing the number of repetitions, and e representing the number of endings
	#END.x — Indicates that the following measures belong to ending number x, and will repeat back to most recent #REP instruction until x matches number of final ending

'|' — Denotes the end of an individual note’s data. [ C.4.1| ]

{x:y} — Defines the parameters of a measure, where x is the measure number and y is the number of notes contained within the measure. [ {1:2} ]

{P(x:y):n} — Indicates that the following measure is a pickup measure, where x and y represents the time signature of the pickup bar, and n represents the number of notes. The time signature should refer only to the pickup bar, and should differ from the actual time signature. For example, to encode a quarter pickup for a piece in 4/4 time: {P(1:4):1}

+ — Indicates that the preceding pitch is sharp. [ F+ = F# / F sharp ] 

- — Indicates that the preceding pitch is flat. [ B- = Bb / B flat ]

n — Indicates that the preceding pitch is natural.  [ Bn = B natural ]

d — Follows the duration value of a note entry to indicate that it is dotted

[ N1.O,N2.O,etc. ].x — Indicates that the following notes are contained in a block chord, with N1, N2, etc. representing each note’s letter name, and O representing the note’s octave number. x represents the duration of the chord

R.x — Indicates a rest, with x representing the rest’s duration

T — Follows the duration value of a note (including d if it is dotted) to indicate the note is tied to the next entry

===========================================



Each measure’s data should be preceded by two values contained in curly braces, separated by a colon (without whitespace), the first value representing the number of the intended measure, and the second value representing the number of notes it contains.

Each note entry requires three values (separated by periods, without whitespace): a character indicating the associated letter name of the note’s scale degree (e.g. “A”, “B”, “C”, etc.), an integer value defining the note’s octave (A0-C8), and an integer value representing the note’s duration (1 = whole, 2 = half, 4 = quarter, etc.). The vertical line symbol should follow each entry. Thus, to encode the first few bars of Beethoven's Für Elise ["EX_01.tiff"], you would write:


#CLEF:treble
#TS:3.8

{P(1:8):2}
E.5.16|
D+.5.16|

{1:6}
E.5.16|
D+.5.16|
E.5.16|
B.5.16|
Dn.5.16|
C.5.16|

{2:5}
A.5.8|
R.16|
C.4.16|
E.4.16|
A.4.16|

{3:5}
B.5.8|
R.16|
E.4.16|
G+.4.16|
B.5.16|


Another example ["EX_02.tiff"] (from Beethoven's Sonata Op. 13), this time with chords:

#CLEF:treble
#TS:4.4
#KS:Cm

{1:4}
[E-.3,G.3,C.4].4T|
[E-.3,G.3,C.4].16d|
[G.3,C.4].32|
[G.3,Bn.3,D.4].16d|
[G.4,C.4,E-.4].32|
[An.3,C.4,E-.4].4
[Bn.3,D.4].8|
R.8|
