--- chords
-- n koch 2020.02.22
-- out1: chord root note
-- out2: chord note 2
-- out 3: chord note 3
-- out 4: chord note 4

major = {0, 2, 4, 5, 7, 9, 11, 12}
harmonicMinor = {0, 2, 3, 5, 7, 8, 10, 12}
dorian = {0, 2, 3, 5, 7, 9, 10, 12}
majorTriad = {0, 4, 7, 12}
dominant7th = {0, 4, 7, 10, 12}
octaves = {-1, 0, 1, 2, 3, 4}
octave = 1

function init()
    root = octave * 12
    third = root + harmonicMinor[3]
    fifth = root + harmonicMinor[4]
    extension = root + 12 + harmonicMinor[2]

    output[1].volts = root / 12
    output[2].volts = third / 12
    output[3].volts = fifth / 12
    output[4].volts = extension / 12
end
