# kOS-Ferram

## Description
An addon for the kOS mod for Kerbal Space Program, which provides an interface with the Ferram Aerospace Research (FAR) mod.
It makes most of the data and methods contained in the Ferram API available for kOS scripts.


## Suffixes

To access the Ferram data structure the basic command to use is `ADDONS:FAR`, followed by the identifier for the desired structure suffix.

- `IAS` - Get - `Scalar` - Returns the current vessel's indicated airspeed.
- `CL` or `LIFTCOEF` - Get - `Scalar` - Returns the current vessel's lift coefficient.
- `CD` or `DRAGCOEF` - Get - `Scalar` - Returns the current vessel's drag coefficient.
- `DYNPRES` - Get - `Scalar` - Returns the current vessel's dynamic pressure, also known as Q.
- `REFAREA` - Get - `Scalar` - Returns the current vessel's reference cross-setional area relative to the airflow.
- `TERMVEL` - Get - `Scalar` - Returns the current vessel's terminal velocity.
- `AOA` or `ANGLEOFATTACK` - Get - `Scalar` - Returns the current vessel's angle of attack, the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's vertical plane. A positive value indicates the vessel is pointing "above" the velocity vector with respect to the vessel's vertical.
- `AOS` or `SIDESLIP` - Get - `Scalar` - Returns the current vessel's angle of sideslip, the angle between the vessel's forward vector and the air-relative velocity vector projected on the vessel's horizontal plane. A positive value indicates the vessel is pointing "left" of the velocity vector with respect to the vessel's vertical.
- `AEROFORCE` - Get - `Vector` - Returns the current vessel's total aerodynamic force in kiloNewtons (kN) in the `SHIP:RAW` frame of reference.
- `AEROFORCEAT( ALTITUDE (Scalar), VELOCITY (Vector) )` - Get - `Vector` - Invokes a method provided by FAR that predicts the total aerodynamic force in kiloNewtons (kN) that the current vessel would experience given two input parameters:  `ALTITUDE` relative to the body's sea level datum and `VELOCITY` relative to the air in the `SHIP:RAW` frame of reference.

## About the `AEROFORCEAT` Suffix


