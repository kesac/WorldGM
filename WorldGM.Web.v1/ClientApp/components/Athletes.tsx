import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

class AthleteViewModel {
    id: number;
    name: string;
    position: string;
    age: number;
    overallRating: number;
}

interface AthletesState {
    athletes: AthleteViewModel[];
    loading: boolean;
}


export class Athletes extends React.Component<RouteComponentProps<{}>, AthletesState> {
    constructor() {
        super();

        this.state = { athletes: [], loading: true };

        fetch('api/Athletes')
            .then(response => {
                /*console.log(response);*/
                return response.json();
            })
            .then(data => {
                this.setState({ athletes: data as AthleteViewModel[], loading: false });
            });
    }

    public render() {

        let athletes = this.state.athletes;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            :  <div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Position</th>
                            <th>Name</th>
                            <th>Age</th>
                            <th>Overall</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                        athletes.map(x => 
                                <tr>
                                    <td>{x.position}</td>
                                    <td><a href={'/api/athlete/' + x.id}>{x.name}</a></td>
                                    <td>{x.age}</td>
                                    <td>{x.overallRating}</td>
                                </tr>
                        )
                    }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }
    /**/

}


