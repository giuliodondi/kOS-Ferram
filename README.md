# kOS-Ferram

Tested in KSP 1.7.3, 1.8.1, 1.9.1 with the latest compatible releases of FAR and kOS.

## Description
An addon for the kOS mod for Kerbal Space Program, which provides an interface with the Ferram Aerospace Research (FAR) mod.
It makes most of the data and methods contained in the Ferram API available for kOS scripts.
This addon has been tested on KSP 1.8.1 for Windows, with kOS version 1.2.2


## Installation

- Make sure a kOS and Ferram Aerospace Research installation are present.
- Just put the main folder inside /GameData


## Suffixes

To access the Ferram data structure the basic command to use is `ADDONS:FAR`, followed by the identifier for the desired structure suffix.

- `IAS` - Get - `Scalar` - Returns the current vessel's indicated airspeed in metres per second (m/s).
- `CL` or `LIFTCOEF` - Get - `Scalar` - Returns the dimensionless current vessel's lift coefficient.
- `CD` or `DRAGCOEF` - Get - `Scalar` - Returns the dimensionless current vessel's drag coefficient.
- `DYNPRES` - Get - `Scalar` - Returns the current vessel's dynamic pressure, also known as Q. 
- `REFAREA` - Get - `Scalar` - Returns the current vessel's reference cross-setional area relative to the airflow. The units are presumed to be square metres (m^2).
- `TERMVEL` - Get - `Scalar` - Returns the current vessel's terminal velocity in metres per second (m/s).
- `AOA` or `ANGLEOFATTACK` - Get - `Scalar` - Returns the current vessel's angle of attack in degrees (°), the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's vertical plane. A positive value indicates the vessel is pointing "above" the velocity vector with respect to the vessel's vertical.
- `AOS` or `SIDESLIP` - Get - `Scalar` - Returns the current vessel's angle of sideslip in degrees (°), the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's horizontal plane. A positive value indicates the vessel is pointing "left" of the velocity vector with respect to the vessel's vertical.
- `AEROFORCE` - Get - `Vector` - Returns the current vessel's total aerodynamic force in kiloNewtons (kN) in the `SHIP:RAW` frame of reference.
- `AEROFORCEAT( ALTITUDE (Scalar), VELOCITY (Vector) )` - Get - `Vector` - Invokes a method provided by FAR that predicts the total aerodynamic force in kiloNewtons (kN) that the current vessel would experience given two input parameters:  `ALTITUDE` relative to the body's sea level datum and `VELOCITY` relative to the air in the `SHIP:RAW` frame of reference.

## About the `AEROFORCEAT` Suffix

The aerodynamic calculations done by Ferram assume that the vessel in the current attitude is moving in the direction specified by the given velocity vector. The vessel's aerodynamic model will be rotated given the vessel's current facing, up and right vectors expressed in the `SHIP:RAW` coordinates, andthe velocity vector will also be interpreted in the `RAW` coordinates.
It is sometimes more intuitive to define the vessel's attitude relative to the air-relative velocity in terms of Angle of Attack and Sideslip angles. If these angles are known, then a way to get the correct velocity vector is to take the ship facing vector, rotate it about the right vector by the angle of attack, rotate the result about the up vector by the sideslip angle and scale this by the desired air-relative velocity value. 

This suffix is useful to predict the aerodynamic force in the future given a specific vessel attitude relative to the expected air-relative velocity vector. However, as mentioned, the calculations use the vessel's attitude *right now*, and know nothing of the attitude some time form now. Therefore, once the aerodynamic force is obtained following the steps above, it must be rotated to match the expected conditions. 


# Build info

To build the source you will need an IDE or compiler that can open Visual Studio projects and solutions. You may setup a build environment for kOS itself as detailed in its repository, and then paste the '/src' folder included in this mod in the kOS build directory, this way the reference folder paths should match.
