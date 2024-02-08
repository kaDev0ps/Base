resource "vsphere_virtual_machine" "test-terra-vm" {
  name             = "test-terra-vm"
  resource_pool_id = "${data.vsphere_resource_pool.pool.id}"
  datastore_id     = "${data.vsphere_datastore.datastore.id}"
  num_cpus = 2
  memory   = 4096
  guest_id = "ubuntu64Guest"
  scsi_type = "lsilogic-sas"
  wait_for_guest_net_timeout = 0

  network_interface {
    network_id   = "${data.vsphere_network.network.id}"
    adapter_type = "${data.vsphere_virtual_machine.template.network_interface_types[0]}"
  }

  disk {
    label            = "test-terra-templ"
    size             = 40
    thin_provisioned = true
  }
  
  clone {
    template_uuid = "${data.vsphere_virtual_machine.template.id}"
 
    customize {
      linux_options {
        host_name = "test-terra-vm"
        domain    = "zb.local"
      }
      
      dns_server_list     = ["172.21.0.4", "77.88.8.8"]
      network_interface {
        ipv4_address = "172.21.0.103"
        ipv4_netmask = 24
      }
 
      ipv4_gateway = "172.21.0.1"
    }
  }
}