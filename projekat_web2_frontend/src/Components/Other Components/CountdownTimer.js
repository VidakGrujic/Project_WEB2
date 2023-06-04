import React from 'react';
import {useCountdown} from './useCountdown';

const CountdownTimer = ({ targetDate }) => {
  const [days, hours, minutes, seconds] = useCountdown(targetDate);

  if (days + hours + minutes + seconds <= 0) {
    return <div>Porudzbina je dostavljena</div>;
  } else {
    return (
        <div>
            {days} dana {hours} sati {minutes} minuta {seconds} sekundi
        </div>
    );
  }
};

export default CountdownTimer;