# Unity Scripts

**IMPORTANT:** Do *not* rename script files. Unity uses the file name to create and access a MonoBehaviour class.

---------------

## CameraFollow.cs
Instructs a camera GameObject to follow a targeted GameObject. This could be a player character, vehicle, or non-player character (NPC). This script does *not* rotate the camera with the targeted GameObject. To achieve that, make the camera GameObject a child of the target GameObject in Unity's Hierarchy window and do *not* add this script.

### Usage
- Add script to any camera GameObject (usually the Main Camera) in a scene.
- Select a GameObject you want to follow as the Target parameter.
- Set the Offset position (usually a negative integer in the Z-axis).

---------------

## VehicleControl.cs
Controls a vehicle's wheels with rotation of wheel meshes using wheel colliders. This script also allows the player to steer the vehicle.

### Usage
- Add script to the parent vehicle GameObject.
- Be sure to set up pairs of wheel meshes (right and left for each axle of your vehicle) in one child GameObject of the parent vehicle GameObject.
- Be sure to set up pairs of wheel colliders (right and left for each axle of your vehicle) in one child GameObject of the parent vehicle GameObject.
- In the script's properties, add the number of axles your vehicle needs controlled.
- For each axle, add a wheel mesh and wheel collder for each wheel on that axle.
- Select at least 1 axle to provide power by checking Motor. (More axles connected to Motor will give your vehicle more power.)
- If you want steering, check Steering on at least one axle.

Additional script properties control the vehicles torque (motor and brake), deceleration force (how quickly the vehicle comes to a stop), and steering angle (how far the wheels can turn when steering the vehicle).

These parameters default to 0. However, here are some suggested values.

- Max Motor Torque = -800 (-/+ changes the direction of the motor)
- Max Steering Angle = 30
- Brake Torque = 400
- Deceleration Force = 200

### Player Controls
- ARROW UP/W = Drive Forward
- ARROW RIGHT/D = Steer Right
- ARROW LEFT/A = Steer Left
- SPACE = Brake

**NOTE:**
If your vehicle's wheel meshes don't align with your vehicle in Play mode, you can manually reorient them by entering additional rotation parameters for the Reorient Wheels property. Change the X, Y, and Z values (typically in increments of 90 degrees) to create additional rotation that will realign your wheel meshes during Play mode.

**NOTE:** If your vehicle tilts too much or flips over easily while turning, try adjusting the vehicle's Mass under the parent GameObject's Rigidbody component. The more mass, the less easily it will be for your vehicle to tip over. You can also adjust your vehicle's Drag in the Rigidbody component to create more or less air resistance against your vehicle.