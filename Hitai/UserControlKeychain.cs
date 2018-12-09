using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai
{
    public partial class UserControlKeychain : UserControl
    {
        private readonly Dictionary<Keypair, ListViewItem> _keyPairToListViewItemMap =
            new Dictionary<Keypair, ListViewItem>();

        private Keychain _keychain;

        public UserControlKeychain() {
            InitializeComponent();
        }

        public Keypair[] SelectedItems {
            get {
                IEnumerable<Keypair> keypairs = listView.SelectedItems
                    .Cast<ListViewItem>()
                    .Select(item => _keyPairToListViewItemMap.Keys
                        .FirstOrDefault(x => _keyPairToListViewItemMap[x] == item));
                Keypair[] enumeration = keypairs as Keypair[] ?? keypairs.ToArray();
                if (enumeration.Any(x => x == null))
                    throw new Exception("Could not find ListItem-respective keypair");
                return enumeration;
            }
        }

        public Keychain Keychain {
            get => _keychain;
            set {
                _keychain = value;
                RefreshListView();
            }
        }

        private void Keychain_OnKeypairRemoved(Keypair keypair) {
            _keyPairToListViewItemMap[keypair].Remove();
            _keyPairToListViewItemMap.Remove(keypair);
        }

        private void Keychain_OnKeypairAdded(Keypair keypair) {
            // private? | id | name | created | expires
            var item = new ListViewItem(new[] {
                keypair.IsPrivate.ToString(),
                keypair.ShortId,
                keypair.UserId,
                keypair.CreationTime.ToShortDateString(),
                keypair.Expires.ToShortDateString()
            });
            _keyPairToListViewItemMap.Add(keypair, item);
            listView.Items.Add(item);
        }

        private void RefreshListView() {
            _keyPairToListViewItemMap.Clear();
            listView.Items.Clear();
            foreach (Keypair keypair in Keychain.Keys) {
                // private? | id | name | created | expires
                var item = new ListViewItem(new[] {
                    keypair.IsPrivate.ToString(),
                    keypair.ShortId,
                    keypair.UserId,
                    keypair.CreationTime.ToShortDateString(),
                    keypair.Expires.ToShortDateString()
                });
                _keyPairToListViewItemMap.Add(keypair, item);
                listView.Items.Add(item);
            }
        }

        private async void UserControlKeychain_Load(object sender, EventArgs e) {
            Keychain = await Keychain.GetInstance();
            Keychain.OnKeypairAdded += Keychain_OnKeypairAdded;
            Keychain.OnKeypairRemoved += Keychain_OnKeypairRemoved;
        }
    }
}
