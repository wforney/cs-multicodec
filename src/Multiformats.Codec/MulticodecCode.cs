namespace Multiformats.Codec;

/// <summary>
/// The multicodec code enumeration
/// </summary>
public enum MulticodecCode : ulong
{
    /// <summary>
    /// Miscellaneous Unknown
    /// </summary>
    [StringValue("<Unknown Multicodec>")]
    Unknown = 0,

    /// <summary>
    /// Miscellaneous Raw
    /// </summary>
    [StringValue("raw")]
    Raw = 0x55,

    /// <summary>
    /// Base 1
    /// </summary>
    Base1 = 0x01,

    ///// <summary>
    ///// Base 2
    ///// </summary>
    //Base2 = Raw,        

    /// <summary>
    /// Base 8
    /// </summary>
    Base8 = 0x07,

    /// <summary>
    /// Base 10
    /// </summary>
    Base10 = 0x09,

    /// <summary>
    /// Base 16
    /// </summary>
    Base16,

    /// <summary>
    /// Base 32
    /// </summary>
    Base32,

    /// <summary>
    /// Base 32 hex
    /// </summary>
    Base32Hex,

    /// <summary>
    /// Base 58 Flickr
    /// </summary>
    Base58Flickr,

    /// <summary>
    /// Base 58 BitCoin
    /// </summary>
    [StringValue("base58btc")]
    Base58BitCoin,

    /// <summary>
    /// Base 64
    /// </summary>
    Base64,

    /// <summary>
    /// Base 64 URL
    /// </summary>
    Base64Url,

    /// <summary>
    /// Serialization CBOR
    /// </summary>
    CBOR = 0x51,

    /// <summary>
    /// Serialization Binary JSON
    /// </summary>
    [StringValue("bjson")]
    BinaryJson,

    /// <summary>
    /// Serialization Universal Binary JSON
    /// </summary>
    [StringValue("ubjson")]
    UniversalBinaryJson,

    /// <summary>
    /// Serialization Protocol Buffers
    /// </summary>
    [StringValue("protobuf")]
    ProtolBuffers = 0x50,

    /// <summary>
    /// Serialization Capn Protocol
    /// </summary>
    [StringValue("capnp")]
    CapnProto,

    /// <summary>
    /// Serialization Flat Buffers
    /// </summary>
    [StringValue("flatbuf")]
    FlatBuffers,

    /// <summary>
    /// Serialization Recursive Length Prefix
    /// </summary>
    [StringValue("rlp")]
    RecursiveLengthPrefix = 0x60,

    /// <summary>
    /// Serialization Message Pack
    /// </summary>
    [StringValue("msgpack")]
    MessagePack,

    /// <summary>
    /// Serialization Binc
    /// </summary>
    [StringValue("binc")]
    Binc,

    /// <summary>
    /// Serialization BenCode
    /// </summary>
    [StringValue("bencode")]
    Bencode = 0x63,

    /// <summary>
    /// Multiformats multicodec
    /// </summary>
    [StringValue("multicodec")]
    Multicodec = 0x30,

    /// <summary>
    /// Multiformats multihash
    /// </summary>
    [StringValue("multihash")]
    Multihash = 0x31,

    /// <summary>
    /// Multiformats multiaddress
    /// </summary>
    [StringValue("multiaddr")]
    Multiaddress = 0x32,

    /// <summary>
    /// Multiformats multibase
    /// </summary>
    [StringValue("multibase")]
    Multibase = 0x33,

    /// <summary>
    /// The IP4 multiaddress
    /// </summary>
    IP4 = 0x04,

    /// <summary>
    /// The IP6 multiaddress
    /// </summary>
    IP6 = 0x29,

    /// <summary>
    /// The TCP multiaddress
    /// </summary>
    TCP = 0x06,

    /// <summary>
    /// The UDP multiaddress
    /// </summary>
    UDP = 0x0111,

    /// <summary>
    /// The DCCP multiaddress
    /// </summary>
    DCCP = 0x21,

    /// <summary>
    /// The SCTP multiaddress
    /// </summary>
    SCTP = 0x84,

    /// <summary>
    /// The UDT multiaddress
    /// </summary>
    UDT = 0x012D,

    /// <summary>
    /// The UTP multiaddress
    /// </summary>
    UTP = 0x012E,

    /// <summary>
    /// The IPFS multiaddress
    /// </summary>
    IPFS = 0x2A,

    /// <summary>
    /// The HTTP multiaddress
    /// </summary>
    HTTP = 0x01E0,

    /// <summary>
    /// The HTTPS multiaddress
    /// </summary>
    HTTPS = 0x01BB,

    /// <summary>
    /// The QUIC multiaddress
    /// </summary>
    QUIC = 0x01CC,

    /// <summary>
    /// The WS multiaddress
    /// </summary>
    WS = 0x01DD,

    /// <summary>
    /// The ONION multiaddress
    /// </summary>
    ONION = 0x01BC,

    /// <summary>
    /// The P2P circuit multiaddress
    /// </summary>
    P2PCircuit = 0x0122,

    /// <summary>
    /// Archiving TAR
    /// </summary>
    Tar,

    /// <summary>
    /// Archiving ZIP
    /// </summary>
    Zip,

    /// <summary>
    /// Imaging PNG
    /// </summary>
    Png,

    /// <summary>
    /// Imaging JPG
    /// </summary>
    Jpg,

    /// <summary>
    /// Video MP4
    /// </summary>
    Mp4,

    /// <summary>
    /// Video MKV
    /// </summary>
    Mkv,

    /// <summary>
    /// IPLD raw git
    /// </summary>
    [StringValue("git-raw")]
    GitRaw = 0x78,

    /// <summary>
    /// IPLD Merkle directed acyclic graph protocol buffers
    /// </summary>
    [StringValue("dag-pb")]
    MerkleDAGProtobuf = 0x70,

    /// <summary>
    /// IPLD Merkle directed acyclic graph CBOR
    /// </summary>
    [StringValue("dag-cbor")]
    MerkleDAGCBOR = 0x71,

    /// <summary>
    /// IPLD Merkle directed acyclic graph JSON
    /// </summary>
    [StringValue("dag-json")]
    MerkleDAGJSON = 0x129,

    /// <summary>
    /// IPLD Ethereum block
    /// </summary>
    [StringValue("eth-block")]
    EthereumBlock = 0x90,

    /// <summary>
    /// IPLD Ethereum block list
    /// </summary>
    [StringValue("eth-block-list")]
    EthereumBlockList = 0x91,

    /// <summary>
    /// IPLD Ethereum transaction trie
    /// </summary>
    [StringValue("eth-tx-trie")]
    EthereumTransactionTrie = 0x92,

    /// <summary>
    /// IPLD Ethereum transaction
    /// </summary>
    [StringValue("eth-tx")]
    EthereumTransaction = 0x93,

    /// <summary>
    /// IPLD Ethereum transaction receipt trie
    /// </summary>
    [StringValue("eth-tx-receipt-trie")]
    EthereumTransactionReceiptTrie = 0x94,

    /// <summary>
    /// IPLD Ethereum transaction receipt
    /// </summary>
    [StringValue("eth-tx-receipt")]
    EthereumTransactionReceipt = 0x95,

    /// <summary>
    /// IPLD Ethereum state trie
    /// </summary>
    [StringValue("eth-state-trie")]
    EthereumStateTrie = 0x96,

    /// <summary>
    /// IPLD Ethereum account snapshot
    /// </summary>
    [StringValue("eth-account-snapshot")]
    EthereumAccountSnapshot = 0x97,

    /// <summary>
    /// IPLD Ethereum storage trie
    /// </summary>
    [StringValue("eth-storage-trie")]
    EthereumStorageTrie = 0x98,

    /// <summary>
    /// IPLD BitCoin block
    /// </summary>
    [StringValue("bitcoin-block")]
    BitcoinBlock = 0xb0,

    /// <summary>
    /// IPLD BitCoin transaction
    /// </summary>
    [StringValue("bitcoin-tx")]
    BitcoinTransaction = 0xb1,

    /// <summary>
    /// IPLD Zcash block
    /// </summary>
    [StringValue("zcash-block")]
    ZcashBlock = 0xc0,

    /// <summary>
    /// IPLD Zcash transaction
    /// </summary>
    [StringValue("zcash-tx")]
    ZcashTransaction = 0xc1,

    /// <summary>
    /// IPLD Stellar block
    /// </summary>
    [StringValue("stellar-block")]
    StellarBlock = 0xd0,

    /// <summary>
    /// IPLD Stellar transaction
    /// </summary>
    [StringValue("stellar-tx")]
    StellarTransaction = 0xd1,

    /// <summary>
    /// IPLD decred block
    /// </summary>
    [StringValue("decred-block")]
    DecredBlock = 0xe0,

    /// <summary>
    /// IPLD decred transaction
    /// </summary>
    [StringValue("decred-tx")]
    DecredTransaction = 0xe1,

    /// <summary>
    /// IPLD torrent information
    /// </summary>
    [StringValue("torrent-info")]
    TorrentInfo = 0x7b,

    /// <summary>
    /// IPLD torrent file
    /// </summary>
    [StringValue("torrent-file")]
    TorrentFile = 0x7c,

    /// <summary>
    /// IPLD Ed25519 public key
    /// </summary>
    [StringValue("ed25519-pub")]
    Ed25519PublicKey = 0xed,
}
