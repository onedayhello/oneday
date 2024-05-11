db.createUser({
    user: 'onedayuser',
    pwd: 'onedaypassword',
    roles: [
        {
            role: 'readWrite',
            db: 'oneday'
        }
    ]
});