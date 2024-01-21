clearscreen.




until false {
	print round(addons:far:ias, 2) + "   " at (0,1).
	print round(addons:far:mach, 2) + "   " at (0,2).
	print round(addons:far:cl, 2) + "   " at (0,3).
	print round(addons:far:cd, 2) + "   " at (0,4).
	print round(addons:far:dynpres, 2) + "   " at (0,5).
	print round(addons:far:refarea, 2) + "   " at (0,6).
	print round(addons:far:termvel, 2) + "   " at (0,7).
	print round(addons:far:aoa, 2) + "   " at (0,8).
	print round(addons:far:aos, 2) + "   " at (0,9).

	print round(addons:far:aeroforceat(SHIP:ALTITUDE, SHIP:VELOCITy:SURFACE):mag, 2) + "   " at (0,11).
	print round(addons:far:aeroforce:mag, 2) + "   " at (0,12).
	print round(addons:far:aerotorque:mag, 2) + "   " at (0,13).
	
	wait 0.1.
}