using kOS.Safe.Encapsulation;
using kOS.Safe.Encapsulation.Suffixes;
using kOS.Safe.Exceptions;
using kOS.Suffixed;
using kOS.Utilities;
using System;
using UnityEngine;

namespace kOS.AddOns.FARAddon
{
    [kOSAddon("FAR")]
    [Safe.Utilities.KOSNomenclature("FerramAddon")]
    public class Addon: Suffixed.Addon
    {
        public Addon(SharedObjects shared) : base(shared)
        {
            InitializeSuffixes();
        }
		
		private void InitializeSuffixes()
        {
		    AddSuffix(new string[] { "IAS" }, new Suffix<ScalarValue>(GetIAS, "Current vessel's Indicated Airspeed."));
		    AddSuffix(new string[] { "MACH" }, new Suffix<ScalarValue>(GetMach, "Current vessel's Mach number."));
            AddSuffix(new string[] { "CL", "LIFTCOEF" }, new Suffix<ScalarValue>(GetLiftCoef, "Current vessel's Lift Coefficient."));
            AddSuffix(new string[] { "CD", "DRAGCOEF" }, new Suffix<ScalarValue>(GetDragCoef, "Current vessel's Drag Coefficient."));
            AddSuffix(new string[] { "DYNPRES" }, new Suffix<ScalarValue>(GetDynPres, "Current vessel's Dynamic Pressure."));
            AddSuffix(new string[] { "REFAREA" }, new Suffix<ScalarValue>(GetRefArea, "Current vessel's reference cross-sectional area relative to the airflow."));
            AddSuffix(new string[] { "TERMVEL" }, new Suffix<ScalarValue>(GetTermVel, "Current vessel's estimated terminal velocity."));
            AddSuffix(new string[] { "AOA", "ANGLEOFATTACK" }, new Suffix<ScalarValue>(GetAOA, "Current vessel's angle of attack relative to the airflow."));
            AddSuffix(new string[] { "AOS", "SIDESLIP" }, new Suffix<ScalarValue>(GetSideslip, "Current vessel's sideslip angle relative to the airflow."));
            AddSuffix(new string[] { "AEROFORCEAT" }, new TwoArgsSuffix<Vector, ScalarValue, Vector>(GetAeroForceAt, "Predicted Aerodynamic force given altitude and airspeed vector relative to the vessel."));
            AddSuffix(new string[] { "AEROFORCE" }, new Suffix<Vector>(GetAeroForce,  "Current aerodynamic force being experienced by the vessel."));
            AddSuffix(new string[] { "AEROTORQUE" }, new Suffix<Vector>(GetAeroTorque, "Current aerodynamic torque being experienced by the vessel."));
        }

        private ScalarValue GetIAS()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARIAS(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("IAS", "Ferram");
        }

        private ScalarValue GetMach()
        {
            if (Available())
            {
                double? result = shared.Vessel.mach;
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("MACH", "Ferram");
        }

        private ScalarValue GetLiftCoef()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARLiftCoef(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("LIFTCOEF", "Ferram");
        }

        private ScalarValue GetDragCoef()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARDragCoef(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("DRAGCOEF", "Ferram");
        }

        private ScalarValue GetDynPres()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARDynPres(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("DYNPRES", "Ferram");
        }

        private ScalarValue GetRefArea()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARRefArea(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("REFAREA", "Ferram");
        }

        private ScalarValue  GetTermVel()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARTermVel(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("TERMVEL", "Ferram");
        }

        private ScalarValue  GetAOA()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARAOA(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("AOA", "Ferram");
        }

        private ScalarValue GetSideslip()
        {
            if (Available())
            {
                double? result = FARWrapper.GetVesselFARSideslip(shared.Vessel);
                if (result != null)
                    return result;
            }
            throw new KOSUnavailableAddonException("AOS", "Ferram");
        }

        private Vector GetAeroForceAt(ScalarValue altitude, Vector Velocity)
        {
            if (Available())
            {
                Vector3d airVelocity = Velocity.ToVector3D();

                Vector3d totalForce = FARWrapper.PredictFARAeroForce(shared.Vessel, airVelocity, altitude);

                if (Double.IsNaN(totalForce.x) || Double.IsNaN(totalForce.y) || Double.IsNaN(totalForce.z))
                {
                    // Don't send NaN into the simulation as it would cause bad things (infinite loops, crash, etc.). I think this case only happens at the atmosphere edge, so the total force should be 0 anyway.
                    return new Vector(Vector3d.zero.x, Vector3d.zero.y, Vector3d.zero.z);
                }

                return new Vector(totalForce.x, totalForce.y, totalForce.z);

               
               
            }
            throw new KOSUnavailableAddonException("AEROFORCEAT", "Ferram");
        }


        private Vector GetAeroForce()
        {
            if (Available())
            {
                Vector3? aeroforce = FARWrapper.GetVesselFARAeroForce(shared.Vessel);
                if (aeroforce != null)
                {
                    Vector3 outvector = (Vector3)aeroforce;
                    return new Vector(outvector.x, outvector.y, outvector.z);
                }
            }
            throw new KOSUnavailableAddonException("AEROFORCE", "Ferram");
        }

        private Vector GetAeroTorque()
        {
            if (Available())
            {
                Vector3? aerotorque = FARWrapper.GetVesselFARAeroForce(shared.Vessel);
                if (aerotorque != null)
                {
                    Vector3 outvector = (Vector3)aerotorque;
                    return new Vector(outvector.x, outvector.y, outvector.z);
                }
            }
            throw new KOSUnavailableAddonException("AEROTORQUE", "Ferram");
        }





        public override BooleanValue Available()
        {
            return FARWrapper.Wrapped();
        }
	}
		
}