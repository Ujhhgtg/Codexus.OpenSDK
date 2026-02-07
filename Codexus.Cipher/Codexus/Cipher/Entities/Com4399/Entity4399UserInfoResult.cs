using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A8 RID: 168
public class Entity4399UserInfoResult
{
	// Token: 0x1700025D RID: 605
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000AF57 File Offset: 0x00009157
	// (set) Token: 0x0600063D RID: 1597 RVA: 0x0000AF5F File Offset: 0x0000915F
	[JsonPropertyName("uid")]
	public long Uid { get; set; }

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000AF68 File Offset: 0x00009168
	// (set) Token: 0x0600063F RID: 1599 RVA: 0x0000AF70 File Offset: 0x00009170
	[JsonPropertyName("idcard")]
	public string IdCard { get; set; } = string.Empty;

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000AF79 File Offset: 0x00009179
	// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000AF81 File Offset: 0x00009181
	[JsonPropertyName("reg_time")]
	public long RegTime { get; set; }

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000AF8A File Offset: 0x0000918A
	// (set) Token: 0x06000643 RID: 1603 RVA: 0x0000AF92 File Offset: 0x00009192
	[JsonPropertyName("validateState")]
	public int ValidateState { get; set; }

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000AF9B File Offset: 0x0000919B
	// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000AFA3 File Offset: 0x000091A3
	[JsonPropertyName("bindedphone")]
	public string BindedPhone { get; set; } = string.Empty;

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000646 RID: 1606 RVA: 0x0000AFAC File Offset: 0x000091AC
	// (set) Token: 0x06000647 RID: 1607 RVA: 0x0000AFB4 File Offset: 0x000091B4
	[JsonPropertyName("idcard_state")]
	public int IdCardState { get; set; }

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000AFBD File Offset: 0x000091BD
	// (set) Token: 0x06000649 RID: 1609 RVA: 0x0000AFC5 File Offset: 0x000091C5
	[JsonPropertyName("realname")]
	public string RealName { get; set; } = string.Empty;

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000AFCE File Offset: 0x000091CE
	// (set) Token: 0x0600064B RID: 1611 RVA: 0x0000AFD6 File Offset: 0x000091D6
	[JsonPropertyName("username")]
	public string Username { get; set; } = string.Empty;

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000AFDF File Offset: 0x000091DF
	// (set) Token: 0x0600064D RID: 1613 RVA: 0x0000AFE7 File Offset: 0x000091E7
	[JsonPropertyName("full_bind_phone")]
	public string FullBindPhone { get; set; } = string.Empty;

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000AFF0 File Offset: 0x000091F0
	// (set) Token: 0x0600064F RID: 1615 RVA: 0x0000AFF8 File Offset: 0x000091F8
	[JsonPropertyName("nck")]
	public string Nickname { get; set; } = string.Empty;

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000650 RID: 1616 RVA: 0x0000B001 File Offset: 0x00009201
	// (set) Token: 0x06000651 RID: 1617 RVA: 0x0000B009 File Offset: 0x00009209
	[JsonPropertyName("avatar_middle")]
	public string AvatarMiddle { get; set; } = string.Empty;

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000B012 File Offset: 0x00009212
	// (set) Token: 0x06000653 RID: 1619 RVA: 0x0000B01A File Offset: 0x0000921A
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; } = string.Empty;

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000B023 File Offset: 0x00009223
	// (set) Token: 0x06000655 RID: 1621 RVA: 0x0000B02B File Offset: 0x0000922B
	[JsonPropertyName("state")]
	public string State { get; set; } = string.Empty;

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000B034 File Offset: 0x00009234
	// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000B03C File Offset: 0x0000923C
	[JsonPropertyName("code")]
	public string AuthCode { get; set; } = string.Empty;

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000B045 File Offset: 0x00009245
	// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000B04D File Offset: 0x0000924D
	[JsonPropertyName("account_type")]
	public string AccountType { get; set; } = string.Empty;

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000B056 File Offset: 0x00009256
	// (set) Token: 0x0600065B RID: 1627 RVA: 0x0000B05E File Offset: 0x0000925E
	[JsonPropertyName("hello")]
	public string WelcomeMessage { get; set; } = string.Empty;

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000B067 File Offset: 0x00009267
	// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000B06F File Offset: 0x0000926F
	[JsonPropertyName("idcard_editable")]
	public bool IdCardEditable { get; set; }

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000B078 File Offset: 0x00009278
	// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000B080 File Offset: 0x00009280
	[JsonPropertyName("id_checked")]
	public bool IdChecked { get; set; }

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000B089 File Offset: 0x00009289
	// (set) Token: 0x06000661 RID: 1633 RVA: 0x0000B091 File Offset: 0x00009291
	[JsonPropertyName("id_checked_real")]
	public bool IdCheckedReal { get; set; }

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x0000B09A File Offset: 0x0000929A
	// (set) Token: 0x06000663 RID: 1635 RVA: 0x0000B0A2 File Offset: 0x000092A2
	[JsonPropertyName("phone_bound")]
	public int PhoneBound { get; set; }

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000B0AB File Offset: 0x000092AB
	// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000B0B3 File Offset: 0x000092B3
	[JsonPropertyName("activated")]
	public bool Activated { get; set; }

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000B0BC File Offset: 0x000092BC
	// (set) Token: 0x06000667 RID: 1639 RVA: 0x0000B0C4 File Offset: 0x000092C4
	[JsonPropertyName("vip_info")]
	public Entity4399VipInfo VipInfo { get; set; } = new();
}