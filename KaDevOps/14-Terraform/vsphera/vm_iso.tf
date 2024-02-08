resource "vsphere_virtual_machine" "test-terra-vm" {
  name             = "test-terra-vm"
  resource_pool_id = "${data.vsphere_resource_pool.pool.id}"
  datastore_id     = "${data.vsphere_datastore.datastore.id}"
  num_cpus = 4
  memory   = 4096
  guest_id = "ubuntu64Guest"
  scsi_type = "lsilogic-sas"
  wait_for_guest_net_timeout = 0
  network_interface {
    network_id   = "${data.vsphere_network.network.id}"
  }
  disk {
    label            = "test-disk"
    size             = 40
    thin_provisioned = true
  }
  cdrom {
    datastore_id = "${data.vsphere_datastore.iso_datastore.id}"
    path         = "ubuntu-20.04.3-live-server-amd64.iso"
  } 
}