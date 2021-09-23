using Sandbox;
using System;

public class DoomController : WalkController
{
	public new float BodyGirth { get; set; } = 32.0f;
	public new float BodyHeight { get; set; } = 56.0f;
	public new float EyeHeight { get; set; } = 41.0f;
	public new float StepSize { get; set; } = 24.0f;

	public DoomController()
	{
		Duck = new Duck( this );
		Unstuck = new Unstuck( this );
	}

	public override BBox GetHull()
	{
		var girth = BodyGirth * 0.5f;
		var mins = new Vector3( -girth, -girth, 0 );
		var maxs = new Vector3( +girth, +girth, BodyHeight );

		return new BBox( mins, maxs );
	}

	// Duck body height 32
	// Eye Height 64
	// Duck Eye Height 28

	protected new Vector3 mins;
	protected new Vector3 maxs;

	public override void SetBBox( Vector3 mins, Vector3 maxs )
	{
		if ( this.mins == mins && this.maxs == maxs )
			return;

		this.mins = mins;
		this.maxs = maxs;
	}

	/// <summary>
	/// Update the size of the bbox. We should really trigger some shit if this changes.
	/// </summary>
	public override void UpdateBBox()
	{
		var girth = BodyGirth * 0.5f;

		var mins = new Vector3( -girth, -girth, 0 ) * Pawn.Scale;
		var maxs = new Vector3( +girth, +girth, BodyHeight ) * Pawn.Scale;

		Duck.UpdateBBox( ref mins, ref maxs, Pawn.Scale );

		SetBBox( mins, maxs );
	}
}
