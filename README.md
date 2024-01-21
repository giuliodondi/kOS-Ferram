# kOS-Ferram


## Description
An addon for the kOS mod for Kerbal Space Program, which provides an interface with the Ferram Aerospace Research (FAR) mod.  
It makes most of the data and methods contained in the Ferram API available for kOS scripts.

Last tested in KSP 1.12.3 and kOS 1.4.0.0  
Compatible with FAR Continued up to 0.16.1.2  
Compatible with [my own fork of FAR continued](https://github.com/giuliodondi/Ferram-Aerospace-Research-modded.git)


## Installation

- Make sure the latest versions of kOS and Ferram Aerospace Research are installed.
- Just put the kOS-Addons folder inside GameData/
- There is a kOS script **kosferramtest.ks** that you can use to test the addon.


## Suffixes

To access the Ferram data structure the basic command to use is `ADDONS:FAR:xxxx`, where xxxx is the identifier of the desired structure suffix.

- `IAS` - Get - `Scalar` - Returns the vessel's indicated airspeed in metres per second (m/s).
- `MACH` - Get - `Scalar` - Returns the vessel's Mach number.
- `CL` or `LIFTCOEF` - Get - `Scalar` - Returns the dimensionless vessel's lift coefficient.
- `CD` or `DRAGCOEF` - Get - `Scalar` - Returns the dimensionless vessel's drag coefficient.
- `DYNPRES` - Get - `Scalar` - Returns the vessel's dynamic pressure, also known as Q. 
- `REFAREA` - Get - `Scalar` - Returns the vessel's reference cross-setional area relative to the airflow. The units are presumed to be square metres (m^2).
- `TERMVEL` - Get - `Scalar` - Returns the vessel's terminal velocity in metres per second (m/s).
- `AOA` or `ANGLEOFATTACK` - Get - `Scalar` - Returns the vessel's angle of attack in degrees (°), the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's vertical plane. A positive value indicates the vessel is pointing "above" the velocity vector with respect to the vessel's vertical.
- `AOS` or `SIDESLIP` - Get - `Scalar` - Returns the vessel's angle of sideslip in degrees (°), the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's horizontal plane. A positive value indicates the vessel is pointing "left" of the velocity vector with respect to the vessel's vertical.
- `AEROFORCE` - Get - `Vector` - Returns the vessel's total aerodynamic force in kiloNewtons (kN) in the `SHIP:RAW` frame of reference.
- `AEROFORCEAT( ALTITUDE (Scalar), VELOCITY (Vector) )` - Get - `Vector` - Invokes a method provided by FAR that predicts the total aerodynamic force in kiloNewtons (kN) that the vessel would experience given two input parameters:  `ALTITUDE` relative to the body's sea level datum and `VELOCITY` relative to the air in the `SHIP:RAW` frame of reference.

### About vessels

The addon now supports non-active vessels, meaning that it can be used in a script running on a kOS CPU other than the one on the active vessel.  
I've only done some shallow preliminary testing on this, I welcome bug reports on this

## About the `AEROFORCEAT` Suffix

The aerodynamic calculations done by Ferram assume that the vessel in the current attitude is moving in the direction specified by the given velocity vector. The vessel's aerodynamic model will be rotated given the vessel's current facing, up and right vectors expressed in the `SHIP:RAW` coordinates, andthe velocity vector will also be interpreted in the `RAW` coordinates.
It is sometimes more intuitive to define the vessel's attitude relative to the air-relative velocity in terms of Angle of Attack and Sideslip angles. If these angles are known, then a way to get the correct velocity vector is to take the ship facing vector, rotate it about the right vector by the angle of attack, rotate the result about the up vector by the sideslip angle and scale this by the desired air-relative velocity value. 

This suffix is useful to predict the aerodynamic force in the future given a specific vessel attitude relative to the expected air-relative velocity vector. However, as mentioned, the calculations use the vessel's attitude *right now*, and know nothing of the attitude some time form now. Therefore, once the aerodynamic force is obtained following the steps above, it must be rotated to match the expected conditions. 


# Build info

To build the source you will need an IDE or compiler that can open Visual Studio projects and solutions. You may setup a build environment for kOS itself as detailed in its repository, and then paste the '/src' folder included in this mod in the kOS build directory, this way the reference folder paths should match.
