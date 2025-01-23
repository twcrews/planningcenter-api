namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyServicesOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"allow_mp3_download": false,
				"calendar_starts_on_sunday": true,
				"ccli": "456",
				"ccli_auto_reporting_enabled": false,
				"ccli_connected": true,
				"ccli_reporting_enabled": false,
				"created_at": "2025-01-01T00:00:00Z",
				"date_format": "US",
				"extra_file_storage_allowed": true,
				"file_storage_exceeded": false,
				"file_storage_extra_charges": null,
				"file_storage_extra_enabled": false,
				"file_storage_size": 123456789,
				"file_storage_size_used": 123456,
				"legacy_id": "789",
				"music_stand_enabled": true,
				"name": "Test",
				"owner_name": "Dan Smith",
				"people_allowed": 200,
				"people_remaining": 100,
				"projector_enabled": false,
				"rehearsal_mix_enabled": true,
				"rehearsal_pack_connected": false,
				"required_to_set_download_permission": "editor",
				"secret": "987654321",
				"time_zone": "America/Chicago",
				"twenty_four_hour_time": false,
				"updated_at": "2025-01-01T01:01:01Z"
			},
			"links": {}
		},
		"included": [],
		"meta": {}
	}
	""";
}