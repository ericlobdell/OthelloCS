const Service = new class service {

    getNewMatch( req: INewMatchRequest ) {
        return this.post( "/api/othello/new", req );
    }

    getMoveResult( req: IMoveRequest ) {
        return this.post( "/api/othello/move", req );
    }

    private post = ( url, data ) => {
        return $.ajax( {
            type: 'POST',
            url: url,
            data: JSON.stringify( data ),
            contentType: 'application/json'
        });
    }
}