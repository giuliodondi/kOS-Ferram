using kOS.Safe.Encapsulation;
using kOS.Safe.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Smooth.Pools;
using UnityEngine;
using System.Reflection;
using KSP.Localization;


namespace kOS.AddOns.FARAddon
{
	public class FARWrapper
	{
		private static bool? wrapped = null;
		private static Type FARAPIType = null;
		private static MethodInfo FARIAS = null;	
        private static MethodInfo FARCL = null;
        private static MethodInfo FARCD = null;
        private static MethodInfo FARDynPres = null;
        private static MethodInfo FARREfArea = null;
        private static MethodInfo FARTermVel = null;
        private static MethodInfo FARAOA = null;
        private static MethodInfo FARSideslip = null;
        private static MethodInfo FARAeroforce = null;
        private static MethodInfo FARAerotorque = null;
        private static MethodInfo FARAeropredict = null;


        private static void init()
        {
			
			FARAPIType = AssemblyLoader.loadedAssemblies.SelectMany(x => x.assembly.GetExportedTypes())
                    .FirstOrDefault(x => x.FullName == "FerramAerospaceResearch.FARAPI");
            if (FARAPIType != null)
            {
				wrapped = true;
			}
			else 
			{
                 UnityEngine.Debug.Log("FAR not loaded");
                wrapped = false; 
            }
			
			
			FARIAS = FARAPIType.GetMethod("ActiveVesselIAS");
			if (FARIAS == null)
			{
				SafeHouse.Logger.Log("FARAPI.ActiveVesselIAS method is null.");
				wrapped = false;
				return;
			}

            FARCL = FARAPIType.GetMethod("ActiveVesselLiftCoeff");
            if (FARCL == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselLiftCoeff method is null.");
                wrapped = false;
                return;
            }

            FARCD = FARAPIType.GetMethod("ActiveVesselDragCoeff");
            if (FARCD == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselDragCoeff method is null.");
                wrapped = false;
                return;
            }

            FARDynPres = FARAPIType.GetMethod("ActiveVesselDynPres");
			if (FARDynPres == null)
			{
				SafeHouse.Logger.Log("FARAPI.ActiveVesselDynPres method is null.");
				wrapped = false;
				return;
			}

            FARREfArea = FARAPIType.GetMethod("ActiveVesselRefArea");
            if (FARREfArea == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselRefArea method is null.");
                wrapped = false;
                return;
            }

            FARTermVel = FARAPIType.GetMethod("ActiveVesselTermVelEst");
            if (FARTermVel == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselTermVelEst method is null.");
                wrapped = false;
                return;
            }

            FARAOA = FARAPIType.GetMethod("ActiveVesselAoA");
            if (FARAOA == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselAoA method is null.");
                wrapped = false;
                return;
            }

            FARSideslip = FARAPIType.GetMethod("ActiveVesselSideslip");
            if (FARSideslip == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselSideslip method is null.");
                wrapped = false;
                return;
            }

            FARAeroforce = FARAPIType.GetMethod("ActiveVesselAerodynamicForce");
			if (FARAeroforce == null)
			{
				SafeHouse.Logger.Log("FARAPI.ActiveVesselAerodynamicForce method is null.");
				wrapped = false;
				return;
			}

            FARAerotorque = FARAPIType.GetMethod("ActiveVesselAerodynamicTorque");
            if (FARAerotorque == null)
            {
                SafeHouse.Logger.Log("FARAPI.ActiveVesselAerodynamicForce method is null.");
                wrapped = false;
                return;
            }

            FARAeropredict = FARAPIType.GetMethod("CalculateVesselAeroForces",  new Type[] { typeof(Vessel), typeof(Vector3).MakeByRefType(), typeof(Vector3).MakeByRefType(), typeof(Vector3), typeof(double) } );
            if (FARAeroforce == null)
            {
                SafeHouse.Logger.Log("FARAPI.CalculateVesselAeroForces method is null.");
                wrapped = false;
                return;
            }

            return;	
		
		}
		
		
		public static double? GetFARIAS()
		{
			return (double?)FARIAS.Invoke(null, new object[] { });
		}
		
		public static double? GetFARLiftCoef()
		{
			return (double?)FARCL.Invoke(null, new object[] { });
		}

        public static double? GetFARDragCoef()
        {
            return (double?) FARCD.Invoke(null, new object[] { });
        }

        public static double? GetFARDynPres()
        {
            return (double?)FARDynPres.Invoke(null, new object[] { });
        }

        public static double? GetFARRefArea()
        {
            return (double?)FARREfArea.Invoke(null, new object[] { });
        }

        public static double? GetFARTermVel()
        {
            return (double?)FARTermVel.Invoke(null, new object[] { });
        }

        public static double? GetFARAOA()
        {
            return (double?)FARAOA.Invoke(null, new object[] { });
        }

        public static double? GetFARSideslip()
        {
            return (double?)FARSideslip.Invoke(null, new object[] { });
        }

        public static Vector3? GetFARAeroForce ()
		{
			return (Vector3?)FARAeroforce.Invoke(null, new object[] { });
		}

        public static Vector3? GetFARAeroTorque()
        {
            return (Vector3?)FARAeroforce.Invoke(null, new object[] { });
        }

        public static Vector3d PredictFARAeroForce(Vessel activeship,Vector3d airVelocity, double altitude)
        {

            if (airVelocity.x == 0d || airVelocity.y == 0d || airVelocity.z == 0d)
            {
                //Debug.LogWarning(string.Format("Trajectories: Getting FAR forces - Velocity: {0} | Altitude: {1}", airVelocity, altitude));
                return Vector3.zero;
            }

            Vector3 worldAirVel = new Vector3((float)airVelocity.x, (float)airVelocity.y, (float)airVelocity.z);
            var parameters = new object[] { activeship, new Vector3(), new Vector3(), worldAirVel, altitude };
            FARAeropredict.Invoke(null, parameters);
            return (Vector3)parameters[1];
        }

        public static BooleanValue Wrapped()
        {
            if (wrapped != null)
            {
                return wrapped;
            }
            else //if wrapped == null
            {
                init();
                return wrapped;
            }
        }
		
	}
}