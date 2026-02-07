using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityComponentDownloadInfoResponse
{
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("itype")]
	public int IType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("mtypeid")]
	public int MTypeId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("stypeid")]
	public int STypeId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sub_entities")]
	public List<EntityComponentDownloadInfoResponseSub> SubEntities { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sub_mod_list")]
	public List<ulong> SubModList { get; set; }

}