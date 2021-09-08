﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_Tewi_しゃがみ強攻撃 : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			int zureX = 0;
			int zureY = -16;

			for (int frame = 0; ; frame++)
			{
				//int koma = frame;
				//int koma = frame / 2;
				int koma = frame / 3;

				if (Ground.I.Picture2.Tewi_しゃがみ強攻撃.Length <= koma)
					break;

				if (frame == 5 * 3)
				{
					Game.I.Player.X += 54 * (Game.I.Player.FacingLeft ? -1.0 : 1.0);
					zureX = 16;
				}

				double x = Game.I.Player.X + zureX * (Game.I.Player.FacingLeft ? -1.0 : 1.0);
				double y = Game.I.Player.Y + zureY;
				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;
				bool facingLeft = Game.I.Player.FacingLeft;

				if (frame == 6 * 3)
				{
					Game.I.Shots.Add(new Shot_OneTime(
						20,
						DDCrashUtils.Rect_CenterSize(
							new D2Point(Game.I.Player.X, Game.I.Player.Y),
							new D2Size(200.0, 120.0)
							)
						));
				}

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.Tewi_しゃがみ強攻撃[koma],
					x - DDGround.ICamera.X,
					y - DDGround.ICamera.Y
				);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
