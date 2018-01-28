using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmitParticles : MonoBehaviour {

	public ParticleSystem ps;
	public Tilemap tileMap;
	private float timePassed = 0.0f;

	[SerializeField]
	private float[] lifetimeMultipliers;

	[SerializeField]
	private float[] lifeLossMult;

	const float baseLifetime = 1.5f;

	// Update is called once per frame
	void Update () 
	{
		if(timePassed > 1 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
		{
			timePassed = 0;
			AdjustEmitterLifetime(GetTileName());
			ps.Emit(1000);
		}

		timePassed += Time.deltaTime;
	}

	private string GetTileName()
	{
		var tileLocation = tileMap.WorldToCell(gameObject.transform.position);
		var tile = tileMap.GetTile(tileLocation);
		return tile.name;
	}

	private void AdjustEmitterLifetime(string tile)
	{
		var tileType = (TileTypes)Enum.Parse(typeof(TileTypes), tile);
		/*
		Initial Emit:
			Metal 	
				- multiply lifetime by 2
				- On wall hit, multiply current lifetime by 0.95
			Hardwood
				- multiply lifetime by 1
				- on wall, 0.85
			Tile
				- multiply lifetime by 1.5
				- wall multiply by 0.9
			Concrete (outside)
				- multiply lifetime by 1.2
				- multiply lifetime by 0.8
			Carpet
				- multiply lifetime by 0.5
				- wall hit, multiply by 0.85
			Grass
				- multiply by 0.6
				- wall hit by 0.8
			Padded Cell
				- multiply by .25
				- multiply by .4
		 */
		 var psMain = ps.main;
		 var psCollision = ps.collision;
		 switch(tileType) 
		 {
			case TileTypes.Hardwood :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Hardwood];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Hardwood];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
			 	break;
			}
			case TileTypes.Concrete :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Concrete];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Concrete];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			case TileTypes.Tile : 
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Tile];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Tile];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			case TileTypes.Metal :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Metal];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Metal];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			case TileTypes.Carpet :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Carpet];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Carpet];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			case TileTypes.Grass :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Grass];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Grass];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			case TileTypes.Padded :
			{
				var lifetime = baseLifetime * lifetimeMultipliers[(int)TileTypes.Padded];
				var lossMult = 1.0f - lifeLossMult[(int)TileTypes.Padded];
				psMain.startLifetime = new ParticleSystem.MinMaxCurve (lifetime, lifetime);
				psCollision.lifetimeLossMultiplier = lossMult;
				break;
			}
			default:
				break;
		 }
	}
}
