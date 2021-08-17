﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの状態を保持する。
	/// プレイヤーのレベルとか保有アイテムといった概念が入ってくることを想定して、独立したクラスとする。
	/// </summary>
	public class GameStatus
	{
		// (テスト時など)特にフィールドを設定せずにインスタンスを生成する使い方を想定して、
		// 全てのパラメータはデフォルト値で初期化すること。

		// Game, Game.Player のフィールド名と被らないように HP -> StartHP のようにすること。

		/// <summary>
		/// プレイヤーの最大HP
		/// </summary>
		public int MaxHP = 10;

		/// <summary>
		/// プレイヤーのHP
		/// </summary>
		public int StartHP = 10;

		/// <summary>
		/// スタート地点の Direction 値
		/// 5 == 中央(デフォルト) == ゲームスタート時
		/// 2 == 下から入場
		/// 4 == 左から入場
		/// 6 == 右から入場
		/// 8 == 上から入場
		/// 101 == ロード地点
		/// </summary>
		public int StartPointDirection = 5;

		/// <summary>
		/// プレイヤーが向いている方向
		/// 2 == 下
		/// 4 == 左
		/// 6 == 右
		/// 8 == 上
		/// </summary>
		public int StartFaceDirection = 2;

		/// <summary>
		/// 最後のマップを退場した方向
		/// 5 == 中央(デフォルト) == ゲーム終了時
		/// 2 == 下から退場
		/// 4 == 左から退場
		/// 6 == 右から退場
		/// 8 == 上から退場
		/// </summary>
		public int ExitDirection = 5;

		public Player.武器_e Start選択武器 = Player.武器_e.NORMAL;
		public ResourcePicture2.Player_e Chara = ResourcePicture2.Player_e.Alice;

		/// <summary>
		/// game_進行・インベントリ(enum)
		/// </summary>
		public enum Inventory_e
		{
			B神奈子を倒した, // テスト用

			// 新しい項目をここへ追加...
		}

		public class S_InventoryFlags
		{
			private List<bool> Flags = new List<bool>();

			public bool this[Inventory_e inventory]
			{
				get
				{
					return (int)inventory < this.Flags.Count ? this.Flags[(int)inventory] : false;
				}

				set
				{
					while (this.Flags.Count <= (int)inventory)
						this.Flags.Add(false);

					this.Flags[(int)inventory] = value;
				}
			}

			public string Serialize()
			{
				return new string(this.Flags.Select(flag => flag ? '1' : '0').ToArray());
			}

			public void Deserialize(string value)
			{
				this.Flags = value.Select(chr => chr == '1').ToList();
			}
		}

		/// <summary>
		/// game_進行・インベントリ
		/// </summary>
		public S_InventoryFlags InventoryFlags = new S_InventoryFlags();

		// <---- prm

		#region Serialize / Deserialize

		public string Serialize()
		{
			List<string> dest = new List<string>();

			// ★★★ シリアライズ_ここから ★★★

			dest.Add("" + this.MaxHP);
			dest.Add("" + this.StartHP);
			dest.Add("" + this.StartPointDirection);
			dest.Add("" + this.StartFaceDirection);
			dest.Add("" + this.ExitDirection);
			dest.Add("" + (int)this.Start選択武器);
			dest.Add("" + (int)this.Chara);
			dest.Add(this.InventoryFlags.Serialize());

			// ★★★ シリアライズ_ここまで ★★★

			return SCommon.Serializer.I.Join(dest.ToArray());
		}

		private void S_Deserialize(string value)
		{
			string[] lines = SCommon.Serializer.I.Split(value);
			int c = 0;

			// ★★★ デシリアライズ_ここから ★★★

			this.MaxHP = int.Parse(lines[c++]);
			this.StartHP = int.Parse(lines[c++]);
			this.StartPointDirection = int.Parse(lines[c++]);
			this.StartFaceDirection = int.Parse(lines[c++]);
			this.ExitDirection = int.Parse(lines[c++]);
			this.Start選択武器 = (Player.武器_e)int.Parse(lines[c++]);
			this.Chara = (ResourcePicture2.Player_e)int.Parse(lines[c++]);
			this.InventoryFlags.Deserialize(lines[c++]);

			// ★★★ デシリアライズ_ここまで ★★★
		}

		public static GameStatus Deserialize(string value)
		{
			GameStatus gameStatus = new GameStatus();
			gameStatus.S_Deserialize(value);
			return gameStatus;
		}

		#endregion

		public GameStatus GetClone()
		{
			return Deserialize(this.Serialize());
		}
	}
}
