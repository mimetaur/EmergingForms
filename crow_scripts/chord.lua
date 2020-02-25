--- chords
-- n koch 2020.02.22
-- out1: chord root note
-- out2: chord note 2
-- out 3: chord note 3
-- out 4: chord note 4

major = {0, 2, 4, 5, 7, 9, 11, 12}
harmonicMinor = {0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17}
dorian = {0, 2, 3, 5, 7, 9, 10, 12}
majorTriad = {0, 4, 7, 12}
dominant7th = {0, 4, 7, 10, 12}
octaves = {-1, 0, 1, 2, 3, 4}
octave = 1

root = octave * 12
chord = {}

function init()
    input[1].mode("change", 1.0, 0.1, "rising")
    input[2].mode("change", 1.0, 0.1, "rising")
    output[2].slew = 0
    output[4].slew = 0

    reset_chord()
    play_chord()
end

input[1].change = function(state)
    output[2].slew = 0.5
    output[4].slew = 0.5

    new_random_note()
    play_chord()
end

input[2].change = function(state)
    reset_chord()
end

function play_chord()
    for i = 1, 4 do
        output[i].volts = n2v(chord[i])
    end
end

function new_random_note()
    local new_note = root + harmonicMinor[math.random(#harmonicMinor)]
    local placement = 2
    if (math.random() > 0.5) then
        placement = 4
    end
    chord[placement] = new_note
end

function reset_chord()
    chord[1] = root
    chord[2] = chord[1] + harmonicMinor[3]
    chord[3] = chord[1] + harmonicMinor[4]
    chord[4] = chord[1] + harmonicMinor[9]
end
