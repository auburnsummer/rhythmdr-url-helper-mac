on open location this_URL
	-- This Python script doesn't do anything at the moment but it'll do something eventually
	set helperPath to path to resource "main.py" in directory "Scripts"
	set helperPathP to POSIX path of helperPath
	
	-- Check if RD is already running.
	global rdIsRunning
	tell application "System Events"
		set ids to bundle identifier of every application process
		if ids contains "com.7thBeat.RhythmDoctorEditor" then
			set rdIsRunning to true
		else
			set rdIsRunning to false
		end if
	end tell
	
	-- If it isn't, we can start it with an argument.
	if rdIsRunning then
		display dialog "Rd is already running, I haven't done this yet!"
	else
		do shell script "open -b com.7thBeat.RhythmDoctorEditor --args " & "\"" & this_URL & "\""
	end if
	
end open location