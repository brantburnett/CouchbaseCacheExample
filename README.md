# Couchbase Cache Example

This is an example of a Couchbase memcached implementation.  It uses
a Couchbase cluster to cache database information to reduce load on your
RDBMS servers.  It also uses dependency injection via Ninject to ensure that
only one request per object is made to the Couchbase cluster per page request.

# Setup

This example expects a Couchbase cluster running on localhost, with the "default"
bucket configured in "Memcached" mode.  You may add settings to Web.config and
adjust Global.asax to test on another cluster.  You may adjust NinjectWebCommon.cs
to use a different bucket name.